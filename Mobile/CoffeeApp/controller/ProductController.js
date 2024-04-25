import { child, get, getDatabase, ref } from "firebase/database";

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