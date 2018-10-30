import pandas as pd

credit_df = pd.read_csv("/home/lucasmd/evento-workspace/experimentos/hist_cred_tratado.csv")

from sklearn.preprocessing import OneHotEncoder, LabelEncoder

credit_df = credit_df.drop(credit_df.columns[0], axis=1)

credit_df_tr = credit_df.drop(columns=['rev', 'n_cycles', 'situacao', 'bloq', 'estado'])

credit_df_cat = credit_df_tr.apply(LabelEncoder().fit_transform)

#credit_df_cat.to_csv("/home/lucasmd/evento-workspace/experimentos/hist_cred_tratado2.csv", index=False)

enc = OneHotEncoder(handle_unknown='ignore')
enc.fit(credit_df_cat)

onehotlabels = enc.transform(credit_df_cat).toarray()

import matplotlib.pyplot as plt
import pandas as pd
from scipy.spatial.distance import cdist

import numpy
from sklearn.cluster import KMeans

from tqdm import tqdm

#x = onehotlabels.iloc[:, :].values
distortions = []
# x_scored = zscore(x)

K = range(4, 40)
for k in tqdm(K):
    kmeanModel = KMeans(n_clusters=k)
    kmeanModel.fit(onehotlabels)
    distortions.append(sum(numpy.min(cdist(onehotlabels, kmeanModel.cluster_centers_, 'euclidean'), axis=1)) / onehotlabels.shape[0])
# Plot the elbow
plt.plot(K, distortions, 'bx-')
plt.xlabel('k')
plt.ylabel('Distortion')
plt.title('The Elbow Method showing the optimal k')
plt.show()

kmeanModel = KMeans(n_clusters=17)
kmeanModel.fit(onehotlabels)

from sklearn.externals import joblib

joblib.dump(kmeanModel, "/home/lucasmd/evento-workspace/experimentos/kmeans_model")

from sklearn.cluster import AffinityPropagation

acModel = AffinityPropagation(damping=0.8, max_iter=1000, convergence_iter=10)
acModel.fit(onehotlabels)

df_centroids = pd.DataFrame(kmeanModel.labels_, columns=['centroid'])

credit_df_tr['centroids'] = kmeanModel.labels_

credit_df_tr['rev'] = credit_df['rev']

credit_df_tr.to_csv("/home/lucasmd/evento-workspace/experimentos/hist_cred_centroides.csv", index=False)