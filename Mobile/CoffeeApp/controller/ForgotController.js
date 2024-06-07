import { child, get, getDatabase, ref, set } from "firebase/database";
import { send, EmailJSResponseStatus } from '@emailjs/react-native';

/**
 * @notice Generate a random 4-digit OTP
 * @returns a random 4-digit OTP
 */
const OTP = () => {
    let otpGenerate = Math.floor(Math.random() * 9000);
    return String(otpGenerate).padStart(4, '0');
}

let otpGenerate = '';

/**
 * @notice Send email that contain OTP to the user
 * @param email email
 * @param user user
 * @param otp otp
 */
const sendEmail = async (email, user, otp) => {
    try {
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
    } catch (err) {
        if (err instanceof EmailJSResponseStatus) {
          console.log('EmailJS Request Failed...', err);
        }
  
        console.log('ERROR', err);
      }
}

/**
 * @notice Send OTP to the user's email
 * @dev Check the email is correct and send the OTP to the user's email
 * @param email email
 * @returns The result of the operation
 */
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