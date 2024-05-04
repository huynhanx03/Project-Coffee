import { child, equalTo, get, getDatabase, orderByChild, query, ref } from "firebase/database";
import { getUserData } from "./StorageController";
import { orderBy } from "firebase/firestore";


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

        vouchersList = vouchersList.filter(voucher => Object.keys(voucher.ChiTiet) == userData.MaNguoiDung);
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

export { getVoucher };
