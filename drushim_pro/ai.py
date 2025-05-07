from flask import Flask, request, jsonify
from transformers import AutoTokenizer, AutoModel
import torch

# טוען את המודל HeBERT
MODEL_NAME = "avichr/heBERT"
tokenizer = AutoTokenizer.from_pretrained(MODEL_NAME)
model = AutoModel.from_pretrained(MODEL_NAME)

app = Flask(__name__)

def get_embedding(text):
    # קידוד הטקסט
    inputs = tokenizer(text, return_tensors="pt", truncation=True, padding=True,  max_length=512)
    with torch.no_grad():
        outputs = model(**inputs)

    # לוקחים את וקטור ה-CLS שמייצג את המשפט כולו
    cls_embedding = outputs.last_hidden_state[:, 0 , :]
    return cls_embedding.squeeze().tolist()

@app.route('/embed', methods=['POST'])
def embed():
    data = request.json
    sentence = data.get("sentence")
    if not sentence:
        return jsonify({"error": "Missing 'sentence' field"}), 400

    embedding = get_embedding(sentence)
    return jsonify({"embedding": embedding})

if __name__ == '__main__':
    app.run(port=5000)