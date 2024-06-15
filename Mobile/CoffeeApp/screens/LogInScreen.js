import { View, Text, Image, Dimensions, Pressable, TouchableOpacity, Modal, ActivityIndicator, ScrollView } from "react-native";
import React, { useState } from "react";
import { colors } from "../theme";
import { widthPercentageToDP as wp, heightPercentageToDP as hp } from "react-native-responsive-screen";
import Button from "../components/button";
import { useNavigation } from "@react-navigation/native";
import { TextInput } from "react-native-paper";
import { handleLogin } from "../controller/LoginController";
import {getUserData} from "../controller/StorageController";
import { useDispatch } from "react-redux";
import { getCart, updateCartWithLastPrice } from "../controller/CartController";
import { addToCartFromDatabase } from "../redux/slices/cartSlice";
import ShowToast from "../components/toast";

const { width, height } = Dimensions.get("window");

export default function LogInScreen() {
    const navigation = useNavigation();
    const dispatch = useDispatch();
    const [isHide, setIsHide] = useState(true);
    const [isLoading, setIsLoading] = useState(false);
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");


    const handleShowPassword = () => {
        setIsHide(!isHide);
    };

    const handleGetCart = async () => {
        const userData = await getUserData();
        const items = await updateCartWithLastPrice();
        if (items) {
            for (const key in items[userData.MaNguoiDung]) {
                dispatch(addToCartFromDatabase(items[userData.MaNguoiDung][key]))
            }
        }
    }

    const Login = async () => {
        setIsLoading(true);
        const account = await handleLogin(username, password);
        if (account) {
            setIsLoading(false);
            handleGetCart()
            navigation.replace('HomeTab')
        }
        else {
            setIsLoading(false);
            ShowToast("error", "Đăng nhập thất bại", "Tài khoản hoặc mật khẩu không đúng!")
        }
    };

    return (
        <ScrollView>
            <View className="flex-1 justify-center items-center">
                <Image
                    className="relative"
                    source={require("../assets/images/bgWelcome.png")}
                    style={{ width: width, height: height }}
                />
                <View className="absolute" style={{top: hp(22)}}>
                    <Text className="text-4xl font-extrabold text-center" style={{ color: colors.primar, marginBottom: hp(10) }}>
                        Chào mừng!
                    </Text>
                    <View className="space-y-5" style={{ width: wp(90) }}>
                        <TextInput
                            mode="outlined"
                            autoCapitalize="none"
                            value={username}
                            onChangeText={(text) => setUsername(text)}
                            label="Tên đăng nhập"
                            activeOutlineColor={colors.primary}
                        />
                        <TextInput
                            mode="outlined"
                            label="Mật khẩu"
                            value={password}
                            onChangeText={(text) => setPassword(text)}
                            secureTextEntry={isHide}
                            activeOutlineColor={colors.primary}
                            right={<TextInput.Icon icon={isHide ? "eye" : "eye-off"} onPress={handleShowPassword} />}
                        />
                        <TouchableOpacity className="mb-3" onPress={() => navigation.navigate("Forgot")}>
                            <Text className="text-right font-bold text-base" style={{ color: colors.active }}>
                                Quên mật khẩu?
                            </Text>
                        </TouchableOpacity>
                        <Button content="Đăng nhập" handle={Login} />
                    </View>
                </View>
                <View className="flex flex-row justify-center items-center mt-20 absolute bottom-10">
                    <Text className="font-semibold">Bạn chưa có tài khoản? </Text>
                    <TouchableOpacity onPress={() => navigation.navigate("Register")}>
                        <Text className="font-semibold" style={{ color: colors.active }}>
                            Đăng ký tại đây!
                        </Text>
                    </TouchableOpacity>
                </View>
                <Modal
                    transparent={true}
                    animationType="fade"
                    visible={isLoading}
                    onRequestClose={() => setIsLoading(false)}>
                    <View className='flex-1 justify-center items-center' style={{backgroundColor: 'rgba(0,0,0,0.5)'}}>
                        <View className='bg-white p-5 rounded-lg justify-center items-center' style={{elevation: 5}}>
                            <ActivityIndicator size="large" color="gray" style={{width: wp(28), height: wp(28)}}/>
                            <Text className='text-base font-semibold' style={{color: colors.text(1)}}>Đang đăng nhập...</Text>
                        </View>
                    </View>
                </Modal>
            </View>
        </ScrollView>
    );
}
