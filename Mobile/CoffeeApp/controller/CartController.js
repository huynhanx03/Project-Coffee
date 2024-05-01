import { getDatabase, ref, remove, set, child, get, } from "firebase/database"
import {getUserData} from "./StorageController"

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
 * @notice Check if an item is present in cart
 * @param item product to be checked
 * @return true if item is present, false otherwise
 */
const itemPresent = async (item) => {
    const userData = await getUserData();
    const dbRef = ref(getDatabase());

    try {
        const cartSnapshot = await get(child(dbRef, `GioHang/${userData.MaNguoiDung}/`));
        const cart = cartSnapshot.val();

        // console.log(cart)
        if (cart) {
            for (const [key, value] of Object.entries(cart)) {
                if (key === item.MaSanPham) {
                    return [true, value.SoLuongGioHang];
                }
            }
        }

        return false;
    } catch (err) {
        console.log(err)
        return err
    }
}

/**
 * @notice Add a new item to cart
 * @dev If item is already present in cart, increase the quantity of the item
 * @param item the item to be added to cart
 */
const setCart = async (item) => {
    const userData = await getUserData()
    try {
        const present = await itemPresent(item);
        const db = getDatabase()

        if (present[0]) {
            set(ref(db, `GioHang/${userData.MaNguoiDung}/${item.MaSanPham}`), {
                TenSanPham: item.TenSanPham,
                Gia: item.Gia,
                HinhAnh: item.HinhAnh,
                KichThuoc: item.KichThuoc,
                MaSanPham: item.MaSanPham,
                SoLuongGioHang: present[1] + item.SoLuongGioHang,
                SoLuong: item.SoLuong
            })
            return
        }

        set(ref(db, `GioHang/${userData.MaNguoiDung}/${item.MaSanPham}`), {
            TenSanPham: item.TenSanPham,
            Gia: item.Gia,
            HinhAnh: item.HinhAnh,
            KichThuoc: item.KichThuoc,
            MaSanPham: item.MaSanPham,
            SoLuongGioHang: item.SoLuongGioHang,
            SoLuong: item.SoLuong
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