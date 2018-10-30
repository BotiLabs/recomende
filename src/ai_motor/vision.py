########### Python 2.7 #############
import httplib, urllib, base64
import requests

from os import listdir
from os.path import isfile, join

df['tags'] = ""

for cidade in cidades:
    onlyfiles = [f for f in listdir("/home/lucasmd/evento-workspace/images/" + cidade) if isfile(join(mypath, f))]
    
    tags = []

    for f in onlyfiles:
        data = open(f, 'rb').read()

        res = requests.post(url='http://westus.api.cognitive.microsoft.com/vision/v2.0/tag?',
                        data=data,
                        headers={'Content-Type': 'application/octet-stream',
                                 'Ocp-Apim-Subscription-Key': '{key}'})
                                 
        tags.append([x['name'] for x in res.json()['tags'][0]])
    df_tags = " ".join(tags)
    cid, est = cidade.split("-")
    
    i = 0
    for _, row in df.iterrows():
        if row['CidadeResidencial'] == cid and row['EstadoResidencial']:
            df['tags'][i] = df_tags
        i += 1

#headers = {
    # Request headers
#    'Content-Type': 'application/json',
#    'Ocp-Apim-Subscription-Key': '{keys}',
#}

#params = urllib.urlencode({
#})

#try:
#    conn = httplib.HTTPSConnection('westus.api.cognitive.microsoft.com')
#    conn.request("POST", "/vision/v2.0/tag?%s" % params, "{\"url\":\"https://thumbs.dreamstime.com/b/makeup-cosmetics-different-makeup-cosmetics-brown-wooden-table-99940393.jpg\"}", headers)
#    response = conn.getresponse()
#    data = response.read()
#    print(data)
#    conn.close()
#except Exception as e:
#    print("[Errno {0}] {1}".format(e.errno, e.strerror))

####################################
