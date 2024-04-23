import { View, Text, Dimensions, TouchableOpacity, Image } from "react-native";
import React, { useState } from "react";
import { widthPercentageToDP as wp, heightPercentageToDP as hp } from "react-native-responsive-screen";
import Button from "../components/button";
import { colors } from "../theme";
import { useNavigation } from "@react-navigation/native";
import { TextInput } from "react-native-paper";
import { Register } from "../controller/RegisterController";
import Toast from "react-native-toast-message";

const { width, height } = Dimensions.get("window");

const RegisterScreen = () => {
    const navigation = useNavigation();
    const [isHide, setIsHide] = useState(true);
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [rePassword, setRePassword] = useState("");

    const handleShowPassword = () => {
        setIsHide(!isHide);
    };

    const handleRegister = async () => {
        if (!username || !email || !password || !rePassword) {
            Toast.show({
                type: "error",
                text1: "Đăng ký thất bại",
                text2: "Vui lòng điền đầy đủ thông tin!",
                topOffset: 70,
                text1Style: {fontSize: 18},
                text2Style: {fontSize: 15},
            })
            return;
        }
        if (password !== rePassword) {
            Toast.show({
                type: "error",
                text1: "Đăng ký thất bại",
                text2: "Mật khẩu không khớp!",
                topOffset: 70,
                text1Style: {fontSize: 18},
                text2Style: {fontSize: 15},
            })
            return;
        }
        const rs = await Register(username, email, password);
        
        Toast.show({
            type: rs[0] ? "success" : "error",
            text1: rs[0] ? 'Đăng ký thành công!' : 'Đăng ký thất bại',
            text2: rs[0] ? "Chuyển hướng đến trang đăng nhập!" : rs[1],
            topOffset: 70,
            text1Style: {fontSize: 18},
            text2Style: {fontSize: 15},
            visibilityTime: 2000,
            onHide: () => rs[0] ? navigation.navigate('Login') : null,
            onPress: () => rs[0] ? navigation.navigate('Login') : null
        })
    };
    return (
        <View className="flex-1 justify-center items-center">
            <Image
                className="relative"
                source={require("../assets/images/bgOther.png")}
                style={{ width: width, height: height }}
            />
            <View className="absolute top-36">
                <Text className="text-4xl font-extrabold text-center mb-20" style-={{ color: colors.primary }}>
                    Chào bạn mới!
                </Text>
                <View className="space-y-3" style={{ width: wp(90) }}>
                    <TextInput
                        mode="outlined"
                        require = {true}
                        autoCapitalize="none"
                        label="Tên đăng nhập"
                        value={username}
                        onChangeText={(text) => setUsername(text)}
                        activeOutlineColor={colors.primary}
                    />
                    <TextInput
                        mode="outlined"
                        autoCapitalize="none"
                        label="Email"
                        value={email}
                        onChangeText={(text) => setEmail(text)}
                        activeOutlineColor={colors.primary}
                    />
                    <TextInput
                        mode="outlined"
                        label="Mật khẩu"
                        value={password}
                        onChangeText={(text) => setPassword(text)}
                        secureTextEntry={isHide}
                        activeOutlineColor="#3b1d0c"
                        right={<TextInput.Icon icon={isHide ? "eye" : "eye-off"} onPress={handleShowPassword} />}
                    />
                    <TextInput
                        mode="outlined"
                        label="Nhập lại mật khẩu"
                        value={rePassword}
                        onChangeText={(text) => setRePassword(text)}
                        secureTextEntry={isHide}
                        activeOutlineColor={colors.primary}
                        right={<TextInput.Icon icon={isHide ? "eye" : "eye-off"} onPress={handleShowPassword} />}
                        className="mb-4"
                    />

                    {/* button register */}
                    <Button content="Đăng ký" handle={handleRegister} />
                </View>

                <View className="mt-10 flex-row justify-between items-center" style={{ width: wp(90) }}>
                    <Text
                        style={{
                            height: 1,
                            borderColor: "rgba(59, 29, 12, 0.4)",
                            borderWidth: 1,
                            width: wp(25),
                        }}></Text>

                    <Text style={{ color: "#8B6122" }}>Hoặc đăng ký với</Text>

                    <Text
                        style={{
                            height: 1,
                            borderColor: "rgba(59, 29, 12, 0.4)",
                            borderWidth: 1,
                            width: wp(25),
                        }}></Text>
                </View>

                <TouchableOpacity
                    className="flex justify-center items-center bg-white border-[1px] rounded-lg py-1 mt-5"
                    style={{ borderColor: "#E8ECF4" }}>
                    <Image source={require("../assets/icons/ggIcon.png")} />
                </TouchableOpacity>
            </View>
            <View className="flex flex-row justify-center items-center absolute bottom-10">
                <Text className="font-semibold">Đã có tài khoản? </Text>
                <TouchableOpacity onPress={() => navigation.goBack()}>
                    <Text className="font-semibold" style={{ color: colors.active }}>
                        Đăng nhập tại đây!
                    </Text>
                </TouchableOpacity>
            </View>
        </View>
    );
};

export default RegisterScreen;
