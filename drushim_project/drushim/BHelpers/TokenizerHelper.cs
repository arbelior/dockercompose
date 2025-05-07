//namespace drushim.BHelpers
//{
//    public class TokenizerHelper
//    {
//        private readonly Dictionary<string, long> _vocab;

//        public TokenizerHelper()
//        {
//            _vocab = LoadVocab("vocab.txt"); 
//        }

//        // פונקציה שטוענת מילון מהמילון של המודל (vocab.txt)
//        private Dictionary<string, long> LoadVocab(string vocabPath)
//        {
//            var vocab = new Dictionary<string, long>();
//            var lines = File.ReadAllLines(vocabPath);
//            for (int i = 0; i < lines.Length; i++)
//            {
//                vocab[lines[i]] = i;
//            }
//            return vocab;
//        }

//        // פונקציה שמבצעת טוקניזציה למילה או משפט
//        public (long[], long[]) Tokenize(string text)
//        {
//            var tokens = new List<long> { _vocab["[CLS]"] }; // פותח עם CLS

//            var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
//            foreach (var word in words)
//            {
//                if (_vocab.ContainsKey(word))
//                {
//                    tokens.Add(_vocab[word]); // מילה קיימת במילון
//                }
//                else
//                {
//                    tokens.Add(_vocab["[UNK]"]); // מילה לא מוכרת
//                }
//            }

//            tokens.Add(_vocab["[SEP]"]); // סוגר עם SEP

//            // מייצר attention mask
//            long[] attentionMask = new long[tokens.Count];
//            for (int i = 0; i < tokens.Count; i++)
//            {
//                attentionMask[i] = 1;
//            }

//            return (tokens.ToArray(), attentionMask);
//        }
//    }
//}
