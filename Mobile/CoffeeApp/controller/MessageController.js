import { getDatabase, ref, onValue, push, get, set, child, orderByChild, query } from "firebase/database";
import { orderBy } from "firebase/firestore";
import {getUserData} from "./StorageController";

/**
 * @notice Get new message id in the database
 * @returns new message id in the database
 */
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

/**
 * @notice Send message and store in the database
 * @param message the message that user want to send
 */
const sendMessage = async (message) => {
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
    const dataUser = await getUserData();

    set(ref(db, `TinNhan/${dataUser.MaNguoiDung}/${newId}`), {
        ThoiGian: formattedDate,
        NguoiGui: dataUser.HoTen,
        NoiDung: message,
        MaKH: dataUser.MaNguoiDung,
    })
}

/**
 * @notice Get the message that user and admin sent in real-time
 * @returns The message of the user
 */
const getMessage = async () => {

    return new Promise((resolve, reject) => {
        getUserData().then(userData => {
            const messageRef = ref(db, `TinNhan/${userData.MaNguoiDung}/`);
            const q = query(messageRef, orderByChild('ThoiGian'));
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