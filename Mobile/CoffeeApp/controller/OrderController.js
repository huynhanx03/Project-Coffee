import { child, get, getDatabase, ref, set } from "firebase/database";
import { getUserData } from "./StorageController";

const getNewId = async () => {
    const dbRef = ref(getDatabase());
    const userData = await getUserData();

    try {
        const ordesrSnapshot = await get(child(dbRef, `DonHang/${userData.MaNguoiDung}/`));
        const orders = ordesrSnapshot.val();

        if (orders) {
            const currentId = parseInt(
                Object.keys(orders)[Object.keys(orders).length - 1].slice(2)
            );
            const newId = "DH" + String(currentId + 1).padStart(4, "0");
            return newId;
        } else {
            return "DH0001";
        }
    } catch (error) {
        console.log(error);
        return error
    }
};

/**
 * @notice Save order to database
 */
const saveOrder = async (products) => {
    const newId = await getNewId();
    const userData = await getUserData();
    const db = getDatabase();

    set(ref(db, `DonHang/${userData.MaNguoiDung}/${newId}`), {
        MaDonHang: newId,
        MaNguoiDung: userData.MaNguoiDung,
        TrangThai: "Chờ xác nhận",
        SanPham: {
            ...products
        }
    });
};

const getOrder = async () => {
    const dbRef = ref(getDatabase())
    const userData = await getUserData()

    try {
        const ordersSnapshot = await get(child(dbRef, `DonHang/${userData.MaNguoiDung}`))
        const orders = ordersSnapshot.val()

        return orders
    } catch (error) {
        console.log(error)
        return error
    }
}

export { saveOrder, getOrder };
