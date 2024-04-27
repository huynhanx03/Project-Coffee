from flask import Flask, request, jsonify
from flask_cors import CORS 
import firebase
import recommend

products = firebase.GetProduct()

app = Flask(__name__)
CORS(app)  

@app.route('/recommend', methods=['POST'])
def recommendHTTP():
    try:
        product_id = request.get_json()
        print(product_id)
        evaluates = []
        
        RS = recommend.HybridRecommender(products, evaluates, k = 5)
        productRecommend = RS.recommend("", product_id)
        print(productRecommend)

        # Chuyển danh sách List<Product> khuyến nghị thành chuỗi JSON và gửi trả về cho ứng dụng C#
        return jsonify(productRecommend)
    
    except Exception as e:
        return jsonify({"error": str(e)})

if __name__ == "__main__":
    app.run(port=5000, debug=True)