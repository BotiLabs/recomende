import pandas as pd
from mlxtend.frequent_patterns import apriori
from mlxtend.frequent_patterns import association_rules

dataset = pd.read_csv("baskets.csv")
dataset_table = dataset.drop(['reseller', 'cycle'], axis=1)
frequent_itemsets = apriori(dataset_table, min_support=0.0045, use_colnames=True)
rules = association_rules(frequent_itemsets, metric="lift", min_threshold=1)
rules.head()