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

export { getCategories, getProducts }