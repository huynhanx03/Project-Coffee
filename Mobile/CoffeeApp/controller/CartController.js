import { getDatabase, ref, remove, set, child, get, } from "firebase/database"
import getUserData from "./StorageController"

/**
 * @notice Get new id for new item in cart
 * @returns new id for new item in cart
 */
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
 * @notice Add a new item to cart
 * @param item the item to be added to cart
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

/**
 * @notice Delete an item from cart
 * @param item the item to be deleted from cart
 * @returns 
 */
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


/**
 * @notice remove all items from cart
 */
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
 * @notice Get all items in cart from database
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