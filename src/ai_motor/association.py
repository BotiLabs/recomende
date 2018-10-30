import pandas as pd  
from apyori import apriori

dataset = pd.read_csv("baskets.csv")

association_rules = apriori(dataset["items"], min_support=0.005, min_confidence=0.2, min_lift=3, min_length=2)