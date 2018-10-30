from flask import Flask
from flask import jsonify

import pandas as pd
import numpy as np
from mlxtend.frequent_patterns import apriori
from mlxtend.frequent_patterns import association_rules
from tqdm import tqdm
import random

print("Reading datasets...")
dataset_clustered_resellers = pd.read_csv("clustered_reseller.csv")
dataset_baskets = pd.read_csv("baskets.csv")
dataset_compras = pd.read_csv("historico_compras_masked.csv")
reseller_clusters = dataset_clustered_resellers.groupby(['centroids'])

centroids_list = []
rules_list = []
default_skus = [18936, 19084, 22462, 70947, 70969, 70968]


print("Computing associations for clusters...")
for centroid, resellers in tqdm(reseller_clusters):
    dataset_centroid = pd.DataFrame(columns=dataset_baskets.columns)
    
    for reseller in resellers['rev']:
        dataset_centroid = dataset_centroid.append(dataset_baskets.loc[dataset_baskets['reseller'] == reseller])
    
    frequent_itemsets = apriori(dataset_centroid.drop(['reseller', 'cycle'], axis=1), min_support=0.0045, use_colnames=True)
    rules = association_rules(frequent_itemsets, metric="lift", min_threshold=1)
    rules_list.append(rules)
    centroids_list.append(dataset_centroid)

app = Flask(__name__)

@app.route("/revendedor/<int:reseller_id>")
def recomendacao(reseller_id):
    if reseller_id < 0 or reseller_id > 5000:
        return False

    bought_items = get_bought_items(reseller_id)
    recs = get_recommendation(reseller_id, bought_items)
    skus = []

    for recomendation in recs:
        skus.append(get_sku(recomendation))

    return jsonify(skus)

def get_bought_items(reseller):
    df = dataset_baskets.loc[dataset_baskets['reseller'] == reseller]
    df = df.drop(['cycle', 'reseller'], axis=1)

    bought_items = []
    
    for item in df.sum().iteritems():
        if item[1] > 0:
            bought_items.append(item[0])

    return bought_items

def get_recommendation(reseller_id, bought_items):
    centroid = dataset_clustered_resellers.loc[dataset_clustered_resellers['rev'] == reseller_id].iloc[0]['centroids']
    rules = rules_list[centroid]
    recommendations = []
    b_recomendations = []

    for item in bought_items:
        recommendation = rules[rules['antecedents'] == {item}]

        if recommendation.any:
            try:
                recommendations.append(next(iter(recommendation['consequents'].iloc[0])))
            except:
                recommendations.append(random.choice(default_skus))
            


    for item in recommendations:
        if item not in b_recomendations:
            b_recomendations.append(item)
            

    qtd_rec = len(b_recomendations)
    
    if qtd_rec < 4:
        needed = 4 - qtd_rec
    
        for x in range(needed):
            b_recomendations.append(default_skus[x])

    return b_recomendations


def get_sku(prod_name):
    item = {}
    
    try:
        if type(prod_name) == str:
            item = dataset_compras.loc[dataset_compras['DESCRICAO'] == prod_name].iloc[0]
        else: 
            item = dataset_compras.loc[dataset_compras['COD_MATERIAL'] == prod_name].iloc[0]
    except:
        item = dataset_compras.loc[dataset_compras['COD_MATERIAL'] == random.choice(default_skus)].iloc[0]


    obj = {"code": item['COD_MATERIAL'].astype(str), "description": item["DESCRICAO"], "price": item["VLR_PRATICADO"].astype(float), "category": item["CATEGORIA"]}

    return obj


if __name__ == '__main__':
    app.run(host='0.0.0.0', port=80)