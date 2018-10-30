from icrawler.builtin import GoogleImageCrawler
import pandas as pd

CSV_PATH = "/home/lucasmd/evento-workspace/experimentos/historico_credito_masked.csv"

df = pd.read_csv(CSV_PATH)

cidades = []
cidade = ""

for _, row in df.iterrows():
    cidade_comp = str(row['CidadeResidencial']) + '-' + str(row['EstadoResidencial'])
    if not cidade_comp in cidades:
        cidade = row['CidadeResidencial']
        cidades.append(cidade_comp)
cidades.append(cidade_comp)


df['tags'] = ""

import requests

from os import listdir
from os.path import isfile, join

my_path = "/home/lucasmd/evento-workspace/images"
import time

for cidade in cidades:
    cur_dir = my_path + "/" + cidade
    
    # Run crawler
    search_key = cidade + " vida"
    google_crawler = GoogleImageCrawler(storage={'root_dir': cur_dir})
    google_crawler.crawl(keyword=search_key, max_num=5)

    # Tag images in azure cognitive services
    onlyfiles = [f for f in listdir(cur_dir) if isfile(join(cur_dir, f))]
    
    tags = []

    for f in onlyfiles:
        data = open(cur_dir + "/" + f, 'rb').read()
        time.sleep(0.5)

        res = requests.post(url='http://westus.api.cognitive.microsoft.com/vision/v2.0/tag?',
                        data=data,
                        headers={'Content-Type': 'application/octet-stream',
                                 'Ocp-Apim-Subscription-Key': 'bc5e793d4a7546cb8b4c69fb1cdf781d'})

        if 'tags' in res.json():
            if res.json()['tags']:
                if not res.json()['tags'][0]['name'] in tags:
                    tags.append(res.json()['tags'][0]['name'])
    df_tags = " ".join(tags)
    cid, est = cidade.split("-")
    
    i = 0
    for _, row in df.iterrows():
        if row['CidadeResidencial'] == cid and row['EstadoResidencial']:
            df['tags'][i] = df_tags
        i += 1
        
df.to_csv("/home/lucasmd/evento-workspace/experimentos/historico_credito_masked_tagged.csv")