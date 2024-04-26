import { getDatabase, ref, onValue, push, get, set, child, orderByChild, query } from "firebase/database";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { orderBy } from "firebase/firestore";
import getUserData from "./StorageController";


const getNewId = async () => {
    const dbRef = ref(getDatabase());
    const userData = await getUserData();
    try {
        const messageSnapshot = await get(child(dbRef, `TinNhan/${userData.MaNguoiDung}`));
        const messages = messageSnapshot.val();
        
        if (messages) {
            const currentId = parseInt(Object.keys(messages)[Object.keys(messages).length - 1].slice(2));
            
            const newId = 'TN' + String(currentId + 1).padStart(4, '0');
            return newId;
        } else {
            return 'TN0001';
        }
    } catch (err) {
        console.log(err);
    }
}

const db = getDatabase();

const sendMessage = async (message) => {
    const currentTime = new Date().toString();
    const newId = await getNewId();
    const dataUser = await getUserData();

    set(ref(db, `TinNhan/${dataUser.MaNguoiDung}/${newId}`), {
        ThoiGian: currentTime,
        NguoiGui: dataUser.HoTen,
        NoiDung: message,
        MaKH: dataUser.MaNguoiDung,
    })
}

const getMessage = async () => {

    return new Promise((resolve, reject) => {
        getUserData().then(userData => {
            const messageRef = ref(db, `TinNhan/${userData.MaNguoiDung}/`);
            const q = query(messageRef, orderByChild('ThoiGian'));
            // console.log(q)
            onValue(q, (snapshot) => {
                const data = snapshot.val();
                resolve(data);
            });
        }).catch(error => {
            reject(error);
        });
    });
}

export { sendMessage, getMessage }