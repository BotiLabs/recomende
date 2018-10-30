import pandas as pd

CSV_PATH = "/home/lucasmd/evento-workspace/experimentos/historico_credito_masked_tagged.csv"

hist_cred_tag = pd.read_csv(CSV_PATH)

COMPRA_PATH = "/home/lucasmd/evento-workspace/experimentos/historico_compras_masked.csv"

hist_compra = pd.read_csv(COMPRA_PATH)

grouped_dataset = hist_cred_tag.groupby(['tags'])

tags = []

for name, _ in grouped_dataset:
    #print name

    in_tags = [tag for tag in name.split(" ") if tag not in tags]
    for tag in in_tags:
        tags.append(tag)
    #for index, row in group.iterrows():
    #group['COD_REVENDEDOR'].unique()
    
    #for idx in group['COD_REVENDEDOR'].unique():
    #    compras = hist_compra.loc[hist_compra['COD_REVENDEDOR'] == idx]
    
for tag in tags:
    hist_compra[tag] = 0
    
for name, group in grouped_dataset:
    #for index, row in group.iterrows():
    #group['COD_REVENDEDOR'].unique()
    tags = []
    in_tags = [tagg for tagg in name.split(" ") if tagg not in tags]
    
    for idx in group['COD_REVENDEDOR'].unique():
        compras = hist_compra.loc[hist_compra['COD_REVENDEDOR'] == idx]

        for index, compra in compras.iterrows():
            for tag in in_tags:
                hist_compra.loc[index, tag] += 1


hist_compra.to_csv("/home/lucasmd/evento-workspace/experimentos/historico_compras_tagged.csv")