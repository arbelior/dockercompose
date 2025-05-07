//using Microsoft.ML.OnnxRuntime.Tensors;
//using Microsoft.ML.OnnxRuntime;
//using drushim.BHelpers;

//namespace drushim.BL
//{
//    public class WordService
//    {
//        public class BertService
//        {
//            private readonly InferenceSession _session;
//            private readonly TokenizerHelper _tokenizer;
//            private readonly List<string> _wordsArray = new List<string>
//        {
//            "שלום", "עבודה", "חיים", "מורה", "ילד", "משפחה", "תלמיד"
//        };

//            public BertService()
//            {
//                _session = new InferenceSession("bert.onnx");
//                _tokenizer = new TokenizerHelper();
//            }

//            public float[] GetEmbedding(string word)
//            {
//                var (ids, mask) = _tokenizer.Tokenize(word);

//                var inputs = new List<NamedOnnxValue>
//            {
//                NamedOnnxValue.CreateFromTensor("input_ids", new DenseTensor<long>(ids, new[] { 1, ids.Length })),
//                NamedOnnxValue.CreateFromTensor("attention_mask", new DenseTensor<long>(mask, new[] { 1, mask.Length }))
//            };

//                using var results = _session.Run(inputs);
//                return results.First().AsTensor<float>().ToArray();
//            }

//            public List<string> FindSimilarWords(string word)
//            {
//                var inputEmbedding = GetEmbedding(word);

//                var similarities = _wordsArray.Select(w =>
//                    (Word: w, Similarity: CosineSimilarity(inputEmbedding, GetEmbedding(w))))
//                    .OrderByDescending(x => x.Similarity)
//                    .Take(5)
//                    .Select(x => x.Word)
//                    .ToList();

//                return similarities;
//            }

//            private double CosineSimilarity(float[] a, float[] b)
//            {
//                double dot = 0, normA = 0, normB = 0;
//                for (int i = 0; i < a.Length; i++)
//                {
//                    dot += a[i] * b[i];
//                    normA += Math.Pow(a[i], 2);
//                    normB += Math.Pow(b[i], 2);
//                }
//                return dot / (Math.Sqrt(normA) * Math.Sqrt(normB));
//            }
//        }
//    }
//}



