import { child, get, getDatabase, ref, set } from "firebase/database";
import { send, EmailJSResponseStatus } from '@emailjs/react-native';

const OTP = () => {
    let otpGenerate = Math.floor(Math.random() * 9000);
    return String(otpGenerate).padStart(4, '0');
}

let otpGenerate = '';

const sendEmail = async (email, user, otp) => {
    try {
        console.log('sending email...')
        await send (
            'service_s8671sg',
            'template_wa15s4b',
            {
                to_name: user,
                message: otp,
                recipient: email,
            }, 
            {
                publicKey: '_TBVrvzhnGTqwNxVm'
            }
        )
        console.log('success');
    } catch (err) {
        if (err instanceof EmailJSResponseStatus) {
          console.log('EmailJS Request Failed...', err);
        }
  
        console.log('ERROR', err);
      }
}

const ForgotPassword = async (email) => {
    const dbRef = ref(getDatabase());
    otpGenerate = OTP();
    try {
        // Get users from database
        const usersSnapshot = await get(child(dbRef, 'NguoiDung/'));
        const users = usersSnapshot.val();
        
        if (users) {
            for (const [userId, userData] of Object.entries(users)) {
                if (userData.Email == email) {
                    console.log('sending email...')
                    await sendEmail(email, userData.HoTen, otpGenerate);
                    return [true, 'Gủi mã OTP thành công', otpGenerate];
                }
            }
            return [false, 'Email không tồn tại', ''];
        } else {
            return [false, "Không có dữ liệu người dùng", ''];
        }
    } catch (error) {
        console.error(error);
        return [false, error, ''];
    }
};

export { ForgotPassword };