import { child, get, getDatabase, ref, set } from "firebase/database";
import AsyncStorage from "@react-native-async-storage/async-storage";
import {getUserData} from "./StorageController";


/**
 * @notice Get new address id in the database
 * @returns new address id in the database
 */
const getNewId = async () => {
    const dbRef = ref(getDatabase());
    const data = await getUserData();
    try {
        const addressesSnapshot = await get(child(dbRef, `DiaChi/${data.MaNguoiDung}`));
        const addresses = addressesSnapshot.val();

        if (addresses) {
            const currentId = parseInt(Object.keys(addresses)[Object.keys(addresses).length - 1].slice(2));

            const newId = "DC" + String(currentId + 1).padStart(4, "0");
            return newId;
        } else {
            return "DC0001";
        }
    } catch (err) {
        console.log(err);
    }
};

/**
 * @notice Add a new address to the database
 * @param name name of the user who owns the address
 * @param phone of the user who owns the address
 * @param detail_address the detail address (street, ward, district, city)
 * @param location the longtitude and latitude of the address
 * @returns the result of the operation
 */
const addAddress = async (name, phone, detail_address, location) => {
    const data = await getUserData();
    const db = getDatabase();
    const newId = await getNewId();
    try {
        await set(ref(db, `DiaChi/${data.MaNguoiDung}/${newId}`), {
            MaDC: newId,
            HoTen: name,
            SoDienThoai: phone,
            DiaChi: detail_address + " " + location.address,
            latitude: location.latitude,
            longtitude: location.longtitude,
            Default: false,
        });
        return [true, "Thêm địa chỉ thành công"];
    } catch (err) {
        console.log(err);
        return [false, "Thêm địa chỉ thất bại"];
    }
};

/**
 * @notice Get the addresses of the user
 * @returns the addresses of the user
 */
const getAddress = async () => {
    const data = await getUserData();
    const dbRef = ref(getDatabase());

    try {
        const addressesSnapshot = await get(child(dbRef, `DiaChi/${data.MaNguoiDung}`));
        const addresses = addressesSnapshot.val();

        return addresses;
    } catch (err) {
        console.log(err);
    }
};

/**
 * @notice Set the default address of the user
 */
const setDefaultAddress = async (key) => {
    const addresses = await getAddress();
    const data = await getUserData();
    const db = getDatabase();

    try {
        await set(ref(db, `DiaChi/${data.MaNguoiDung}/${key}`), {
            ...addresses[key],
            Default: true,
        });

        for (const addressKey in addresses) {
            if (addressKey !== key) {
                await set(ref(db, `DiaChi/${data.MaNguoiDung}/${addressKey}`), {
                    ...addresses[addressKey],
                    Default: false,
                });
            }
        }
    } catch (err) {
        console.log(err)
        return err;
    }
};

export { addAddress, getAddress, setDefaultAddress };
