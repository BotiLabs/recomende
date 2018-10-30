import pandas as pd
from mlxtend.frequent_patterns import apriori
from mlxtend.frequent_patterns import association_rules
from tqdm import tqdm

dataset_clustered_resellers = pd.read_csv("clustered_reseller.csv")
dataset_baskets = pd.read_csv("baskets.csv")
dataset_compras = pd.read_csv("historico_compras_masked.csv")

reseller_clusters = dataset_clustered_resellers.groupby(['centroids'])

centroids_list = []
rules_list = []

for centroid, resellers in tqdm(reseller_clusters):
    dataset_centroid = pd.DataFrame(columns=dataset_baskets.columns)
    
    for reseller in resellers['rev']:
        dataset_centroid = dataset_centroid.append(dataset_baskets.loc[dataset_baskets['reseller'] == reseller])
    
    frequent_itemsets = apriori(dataset_centroid.drop(['reseller', 'cycle'], axis=1), min_support=0.0045, use_colnames=True)
    rules = association_rules(frequent_itemsets, metric="lift", min_threshold=1)
    rules_list.append(rules)
    centroids_list.append(dataset_centroid)
    
    
def get_recommendation(reseller_id, bought_items):
    centroid = dataset_clustered_resellers.loc[dataset_clustered_resellers['rev'] == reseller_id].iloc[0]['centroids']
    rules = rules_list[centroid]
    recommendations = []
    
    for item in bought_items:
        recommendation = rules[rules['antecedents'] == {item}]
        recommendations.append(next(iter(recommendation['consequents'].iloc[0])))
    
    return recommendations


def get_sku(prod_name):
    return dataset_compras.loc[dataset_compras['DESCRICAO'] == prod_name].iloc[0]['COD_MATERIAL']


def get_bought_items(reseller):
    df = dataset_baskets.loc[dataset_baskets['reseller'] == reseller]
    df = df.drop(['cycle', 'reseller'], axis=1)

    bought_items = []
    
    for item in df.sum().iteritems():
        if item[1] > 0:
            bought_items.append(item[0])

    return bought_items

get_bought_items(84)
recs = get_recommendation(454, ['A.R.T. DEO COLÔNIA 95 ML', 'SOUL KISS ME BATOM HIDRATANTE FPS 10 ROSA RETRÔ 3,5G'])

for rec in recs:
    print(get_sku(rec))