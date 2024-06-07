import { child, equalTo, get, getDatabase, orderByChild, query, ref, update } from "firebase/database";
import { getUserData } from "./StorageController";


/**
 * @notice Get the vouchers list that user obtained
 * @dev Get all the voucher then filter the voucher by user id
 * @returns The vouchers list that user obtained
 */
const getVoucher = async () => {
    const userData = await getUserData();
    const dbRef = ref(getDatabase());
    const epochCurrent = new Date().getTime();
    try {
        const vouchersSnapshot = await get(child(dbRef, `PhieuGiamGia`))
        const vouchers = vouchersSnapshot.val();

        let vouchersList = [];
        Object.keys(vouchers).forEach(key => {
            vouchersList.push(vouchers[key]);
        });

        vouchersList = vouchersList.filter(voucher => Object.keys(voucher.ChiTiet).includes(userData.MaNguoiDung));
        vouchersList = vouchersList.filter(voucher => voucher.ChiTiet[userData.MaNguoiDung].TrangThai == 'Chưa sử dụng')
        vouchersList = vouchersList.filter(voucher => {
            const formattedExpiryDate = voucher.NgayHetHan.split('/').reverse().join('-');
            const expiredDate = new Date(formattedExpiryDate).getTime();

            return expiredDate + 61199000 >= epochCurrent;
            
        });
        return vouchersList
    } catch (error) {
        console.log(error);
        return error;
    }
};

/**
 * @notice Update the voucher status
 * @param voucherId The id of the voucher
 */
const updateVoucherUsed = async (voucherId) => {
    const db = getDatabase();
    const userData = await getUserData();

    try {
        update(ref(db, `PhieuGiamGia/${voucherId}/ChiTiet/${userData.MaNguoiDung}/`), {
            TrangThai: 'Đã sử dụng'
        })
    } catch (error) {
        console.log(error);
        return error;
    }
}

export { getVoucher, updateVoucherUsed };
