import { child, get, getDatabase, orderByChild, query, ref, set, update, equalTo } from "firebase/database";
import { getUserData } from "./StorageController";

const getNewId = async () => {
    const dbRef = ref(getDatabase());

    try {
        const ordesrSnapshot = await get(child(dbRef, `DonHang/`));
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
const saveOrder = async (products, total, transFee, addressData) => {
    const currentDate = new Date();
    const options = { 
        day: '2-digit', 
        month: '2-digit', 
        year: 'numeric', 
        hour: '2-digit', 
        minute: '2-digit', 
        second: '2-digit',
        hour12: false
      };
    const formattedDate = currentDate.toLocaleString('vi-VN', options);
    const newId = await getNewId();
    const userData = await getUserData();
    const db = getDatabase();

    let productObj = {};
    for (const product of products) {
        productObj = {...productObj, [product.MaSanPham]: product }
    }


    set(ref(db, `DonHang/${newId}/`), {
        MaDonHang: newId,
        MaNguoiDung: userData.MaNguoiDung,
        TrangThai: "Chờ xác nhận",
        SanPham: {
            ...productObj
        },
        ThanhTien: total,
        PhiVanChuyen: transFee,
        NgayTaoDon: formattedDate,
        DiaChiGiaoHang: addressData
    });
};

/**
 * @notive Get the order of the user
 * @returns The order of the user
 */
const getOrder = async () => {
    const db = getDatabase()
    const userData = await getUserData();
    try {
        const orderRef = ref (db, 'DonHang')

        const filteredQuery = query(orderRef, orderByChild('MaNguoiDung'), equalTo(userData.MaNguoiDung))
        const ordersSnapshot = await get(filteredQuery)
        const orders = ordersSnapshot.val()

        return orders
    } catch (error) {
        console.log(error)
        return error
    }
}

/**
 * @notice Set the status of the order
 * @param orderId The id of the order
 */
const setStatusOrder = async (orderId) => {
    const db = getDatabase()

    try {
        update(ref(db, `DonHang/${orderId}`), {
            TrangThai: "Đã nhận hàng"
        })
    } catch (error) {
        console.log(error)
        return error
    }
}

export { saveOrder, getOrder, setStatusOrder };
