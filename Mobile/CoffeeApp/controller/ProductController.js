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

export { getCategories, getProducts, getProductsSale, getProductDetail }