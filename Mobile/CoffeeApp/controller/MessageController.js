import { getDatabase, ref, onValue, push, get, set, child, orderByChild, query } from "firebase/database";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { orderBy } from "firebase/firestore";

const getMakh = async () => {
    try {
        const jsonValue = await AsyncStorage.getItem("user");
        return jsonValue != null ? JSON.parse(jsonValue) : null;
    } catch (error) {
        console.log(error);
        return error;
    }
};

const getNewId = async () => {
    const dbRef = ref(getDatabase());
    const userData = await getMakh();
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
    const dataUser = await getMakh();

    set(ref(db, `TinNhan/${dataUser.MaNguoiDung}/${newId}`), {
        ThoiGian: currentTime,
        NguoiGui: dataUser.HoTen,
        NoiDung: message,
        MaKH: dataUser.MaNguoiDung,
    })
}

const getMessage = async () => {

    return new Promise((resolve, reject) => {
        getMakh().then(userData => {
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

export { sendMessage, getMessage, getMakh }