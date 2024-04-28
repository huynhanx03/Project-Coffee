import { getDatabase, ref, remove, set, child, get, } from "firebase/database"
import getUserData from "./StorageController"


const getNewId = async () => {
    const dbRef = ref(getDatabase())
    const userData = await getUserData()
    try {
        const productSnapshot = await get(child(dbRef, `GioHang/${userData.MaNguoiDung}`))
        const products = productSnapshot.val()

        if (products) {
            const currentId = parseInt(Object.keys(products)[Object.keys(products).length - 1].slice(2))

            const newId = 'SP' + String(currentId + 1).padStart(4, '0')
            return newId
        } else {
            return 'SP0001'
        }
    } catch (err) {
        console.log(err)
    }

}

/**
 * Set item to cart in database
 */
const setCart = async (item) => {
    const userData = await getUserData()
    try {
        const db = getDatabase()
        set(ref(db, `GioHang/${userData.MaNguoiDung}/${item.MaSanPham}`), {
            TenSanPham: item.TenSanPham,
            Gia: item.Gia,
            HinhAnh: item.HinhAnh,
            KichThuoc: item.KichThuoc,
            MaSanPham: item.MaSanPham,
            SoLuongGioHang: item.SoLuongGioHang,
        })
    } catch (error) {
        console.log(error)
        return error
    }
}

const deleteItemCard = async (item) => {
    if (!item || !item.MaSanPham) {
        throw new Error('Sản phẩm không tồn tại');
    }
    const userData = await getUserData()
    try {
        const db = getDatabase()
        remove(ref(db, `GioHang/${userData.MaNguoiDung}/${item.MaSanPham}`))
    } catch (error) {
        console.log(error)
        return error
    }
}

const removeItemCart = async () => {
    const userData = await getUserData()
    try {
        const db = getDatabase()
        remove(ref(db, `GioHang/${userData.MaNguoiDung}`))
    } catch (error) {
        console.log(error)
        return error
    }

}


/**
 * Get cart from database
 */
const getCart = async () => {
    try {
        const dbRef = ref(getDatabase())

        const cartSnapshot = await get(child(dbRef, 'GioHang/'));
        const cart = cartSnapshot.val()
    
        return cart;
    } catch(err) {
        console.log(err);
        return err;
    }
}

export { setCart, getCart, deleteItemCard, removeItemCart }