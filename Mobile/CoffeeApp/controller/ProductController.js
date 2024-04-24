import { child, get, getDatabase, ref } from "firebase/database";


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

export { getProducts }