import pandas as pd
import numpy as np
from sklearn.preprocessing import LabelEncoder, OneHotEncoder
from tqdm import tqdm

dataset = pd.read_csv("historico_compras_masked.csv")
dataset = dataset.loc[0:50000]

labelEncoder_X = LabelEncoder()
cat = labelEncoder_X.fit_transform(dataset.iloc[:, 34].values)
cat = cat.reshape(-1, 1)
oneHotEncoder = OneHotEncoder()
cat = oneHotEncoder.fit_transform(cat).toarray()

for index in range(cat.shape[1]):
    dataset[labelEncoder_X.classes_[index]] = cat[:, index]

grouped_dataset = dataset.groupby(['COD_REVENDEDOR', "COD_CICLO"])
final_dataset = pd.DataFrame()

i = 0
row_list = []

for name, group in tqdm(grouped_dataset):    
    mgroup = group.sum()
    row = {"cycle": name[1], "reseller": name[0]}
    
    for index in range(cat.shape[1]):
        qty = mgroup[labelEncoder_X.classes_[index]]
        
        if qty > 0:
            qty = 1
            
        row[labelEncoder_X.classes_[index]] = qty
        
    row_list.append(row)
    i+=1
    
final_dataset = pd.DataFrame(row_list)
final_dataset.to_csv("baskets.csv", sep=",", index=False)