import pandas as pd

credit_df = pd.read_csv("/home/lucasmd/evento-workspace/experimentos/historico_credito_masked.csv")

final_dataset = pd.DataFrame(columns = ['rev', 'n_cycles', 'cycles_since', 'maior_n_cycles', 
                                        'situacao', 'foi_inativo',
                                        'bloq', 'cidade', 'estado', 'creditoI', 'creditoF'])

i = 0
rev = ""
n_cycles = 0
maior_n_cycles = 0
cycles_since = 0
ant = {}
inativo = 0

for index, row in credit_df.iterrows():
    if rev == "":
        rev = row['COD_REVENDEDOR']
    if not row['COD_REVENDEDOR'] == rev:
        linha = {"rev": rev, 
               "n_cycles": ant['CiclosInatividade'],
               "cycles_since": cycles_since,
               "maior_n_cycles": maior_n_cycles,
               "situacao": ant['Situacao'],
               "foi_inativo": inativo,
               "bloq": ant['Bloqueado'],
               "cidade": ant['CidadeResidencial'],
               "estado": ant['EstadoResidencial'],
               "creditoI": ant['CreditoInicial'],
               "creditoF": ant['LimiteCredito'],
               }
        final_dataset.loc[i] = linha
        i += 1
        rev = row['COD_REVENDEDOR']
        maior_n_cycles = 0
        cycles_since = 0
        inativo = 0
    if row['Situacao'] == "Inativo":
        inativo = 1
    if maior_n_cycles < row['CiclosInatividade']:
        maior_n_cycles = row['CiclosInatividade']
        cycles_since = 0
    else:
        cycles_since += 1
    ant = row
    
linha = {"rev": rev, 
           "n_cycles": ant['CiclosInatividade'],
           "cycles_since": cycles_since,
           "maior_n_cycles": maior_n_cycles,
           "situacao": ant['Situacao'],
           "foi_inativo": inativo,
           "bloq": ant['Bloqueado'],
           "cidade": ant['CidadeResidencial'],
           "estado": ant['EstadoResidencial'],
           "creditoI": ant['CreditoInicial'],
           "creditoF": ant['LimiteCredito'],
           }
final_dataset.loc[i] = linha

final_dataset.to_csv("/home/lucasmd/evento-workspace/experimentos/hist_cred_tratado.csv")

from sklearn.preprocessing import OneHotEncoder, LabelEncoder

credit_df_cat = final_dataset.apply(LabelEncoder().fit_transform)

enc = OneHotEncoder(handle_unknown='ignore')
enc.fit(credit_df_cat)

onehotlabels = enc.transform(credit_df_cat).toarray()
onehotlabels.shape