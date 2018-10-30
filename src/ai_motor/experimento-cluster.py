import pandas as pd

from sklearn.preprocessing import OneHotEncoder, LabelEncoder

credit_df = pd.read_csv("/home/lucasmd/evento-workspace/experimentos/historico_credito_masked.csv")

compras_df = pd.read_csv("/home/lucasmd/evento-workspace/experimentos/historico_compras_masked.csv")

credit_df = credit_df[['COD_REVENDEDOR','Situacao',
                       'CiclosInatividade','Bloqueado','CidadeResidencial',
                       'AceitaSMS','EstadoResidencial','CodigoSetor','CodigoSubSetor',
                       'CreditoInicial','LimiteCredito','ScoreCredito','Restricao']]

credit_df_cat = credit_df.apply(LabelEncoder().fit_transform)
enc = OneHotEncoder(handle_unknown='ignore')
enc.fit(credit_df_cat)

onehotlabels = enc.transform(credit_df_cat).toarray()
onehotlabels.shape

from sklearn.neighbors import NearestNeighbors

nbrs = NearestNeighbors(n_neighbors=100, algorithm='ball_tree').fit(onehotlabels)

distances, indices = nbrs.kneighbors(onehotlabels)