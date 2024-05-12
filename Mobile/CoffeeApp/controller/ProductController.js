import { child, get, getDatabase, ref } from "firebase/database";

/**
 * @notice Get the categories of the products
 * @returns The result of the operation
 */
const getCategories = async () => {
    const dbRef = ref(getDatabase());
    try {
        const categoriesSnapshot = await get(child(dbRef, 'LoaiSanPham/'))
        const categories = categoriesSnapshot.val()

        return categories
    } catch (error) {
        console.log(error)
        return error
    }

}

/**
 * @notice Get the products from the database
 * @returns The result of the operation
 */
const getProducts = async () => {
    const dbRef = ref(getDatabase());
    try {
        const productsSnapshot = await get(child(dbRef, 'SanPham/'))
        const products = productsSnapshot.val()

        return products
    } catch (error) {
        console.log(error)
        return error
    }
}


/**
 * @notice Get the products on sale
 * @returns The products on sale
 */
const getProductsSale = async () => {
    const dbRef = ref(getDatabase());

    try {
        const productsSaleSnapshot = await get(child(dbRef, 'SanPhamGiamGiaHomNay/'))
        const productsSale = productsSaleSnapshot.val()

        return productsSale
    } catch (error) {
        console.log(error)
        return error
    }
}

/**
 * @notice Get the detail of the product by id
 * @returns The detail of the product by id
 */
const getProductDetail = async () => {
    const dbRef = ref(getDatabase());
    const productsSale = await getProductsSale();

    try {
        const productDetailPromises = Object.values(productsSale).map(async (product) => {
            const productDetailSnapshot = await get(child(dbRef, `SanPham/${product.MaSanPham}`));
            const productDetail = productDetailSnapshot.val();
            return { ...productDetail, PhanTramGiam: product.PhanTramGiam };
        });

        const productDetailList = await Promise.all(productDetailPromises);
        return productDetailList;
    } catch (error) {
        console.error(error);
        return [];
    }
};

/**
 * @notice Get the detail of a product by id
 * @param productId The id of product
 * @returns 
 */
const getProductDetailById = async (productId) => {
    const dbRef = ref(getDatabase());
    
    try {
        const productSnapshot = await get(child(dbRef, `SanPham/${productId}`));
        const product = productSnapshot.val();

        return product
    } catch (error) {
        console.log(error)
        return error
    }
}

/**
 * @notice Get the list of product Id that have been sold the most
 * @returns The list of product Id that have been sold the most
 */
const getProductsBestSeller = async () => {
    const db = getDatabase();
    let productsList = [];
    try {
        const ordersSnapshot = await get(child(ref(db), "DonHang/"));
        const orders = ordersSnapshot.val();

        for (const key in orders) {
            if (orders[key].TrangThai === 'Đã nhận hàng') {
                productsList.push(...Object.values(orders[key].SanPham));
            }
        }

        const totalSold = {};

        productsList.map((product) => {
            if (totalSold[product.MaSanPham]) {
                totalSold[product.MaSanPham] += product.SoLuong;
            } else {
                totalSold[product.MaSanPham] = product.SoLuong;
            }
        });
        const sortedValues = Object.values(totalSold).sort().reverse();
        
        for (const key in totalSold) {
            for (let i = 0; i < sortedValues.length; i++) {
                if (totalSold[key] === sortedValues[i]) {
                    sortedValues[i] = key;
                    break;
                }
            }
        }
        
        return sortedValues;
    } catch(err) {
        console.log(err);
        return err
    }
}

export { getCategories, getProducts, getProductsSale, getProductDetail, getProductDetailById, getProductsBestSeller }