import { useNavigation } from "@react-navigation/native";
import React, { useEffect, useRef, useState } from "react";
import { Dimensions, Image, Text, TouchableOpacity, View } from "react-native";
import { TextInput } from "react-native-paper";
import { colors } from "../theme";
import { useSelector, useDispatch } from "react-redux";
import { setOTP } from "../redux/slices/otpSlice";

const { width, height } = Dimensions.get("window");

const VerifyScreen = () => {
    const navigation = useNavigation();
    const dispatch = useDispatch();
    const otp = useSelector((state) => state.otp.otp);
    const [otpInput, setOTPInput] = useState("");
    const [isOK, setIsOK] = useState(false);
    const inputRef1 = useRef();
    const inputRef2 = useRef();
    const inputRef3 = useRef();
    const inputRef4 = useRef();

    useEffect(() => {
        if (otpInput.length === 4) {
            if (otpInput === otp) {
                setIsOK(true);
                setTimeout(() => {
                    navigation.navigate('ChangePasswordForgot')
                    dispatch(setOTP(''))
                }, 2000);
            } else {
                setIsOK(false);
                inputRef1.current.focus();
            }
        }
    }, [otpInput]);

    const setOTPAndFocus = (e, ref) => {
        setOTPInput((text) => text + e);
        ref.current.focus();
    }

    return (
        <View className="flex-1 justify-center items-center">
            <Image source={require("../assets/images/bgOther.png")} style={{ width: width, height: height }} />

            <View className="absolute top-52">
                <View className="flex justify-center items-center">
                    <Text className="text-3xl font-bold">Xác nhận OTP</Text>
                    <Text>Nhập mã xác nhận đã được gửi về email của bạn</Text>
                </View>
                <View className="mt-20 flex-row space-x-5 justify-between">
                    <TextInput
                        mode="outlined"
                        keyboardType="number-pad"
                        ref={inputRef1}
                        activeOutlineColor="#3b1d0c"
                        className="mb-5 text-center"
                        onChangeText={(e) => setOTPAndFocus(e, inputRef2)}
                    />
                    <TextInput
                        mode="outlined"
                        keyboardType="number-pad"
                        ref={inputRef2}
                        activeOutlineColor="#3b1d0c"
                        className="mb-5 text-center"
                        textAlign="center"
                        onChangeText={(e) => setOTPAndFocus(e, inputRef3)}
                    />
                    <TextInput
                        mode="outlined"
                        keyboardType="number-pad"
                        ref={inputRef3}
                        activeOutlineColor="#3b1d0c"
                        className="mb-5 text-center"
                        textAlign="center"
                        onChangeText={(e) => setOTPAndFocus(e, inputRef4)}
                    />
                    <TextInput
                        mode="outlined"
                        keyboardType="number-pad"
                        ref={inputRef4}
                        activeOutlineColor="#3b1d0c"
                        className="mb-5 text-center"
                        textAlign="center"
                        onChangeText={(e) => setOTPAndFocus(e, inputRef4)}
                    />
                </View>

                {/* <Button content='Send' handle={() => navigation.navigate('Verify')}/> */}
            </View>

            <View className="absolute bottom-10 flex-row">
                <Text className="font-semibold">Bạn nhớ mật khẩu rồi sao?</Text>

                <TouchableOpacity onPress={() => navigation.navigate("Login")}>
                    <Text className="font-semibold" style={{ color: colors.active }}>
                        {" "}
                        Đăng nhập ngay
                    </Text>
                </TouchableOpacity>
            </View>
        </View>
    );
};

export default VerifyScreen;
