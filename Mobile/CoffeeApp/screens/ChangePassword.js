import { useNavigation } from "@react-navigation/native";
import React, { useEffect, useState } from "react";
import {
    Dimensions,
    ScrollView,
    Text,
    TouchableOpacity,
    View,
    TextInput,
} from "react-native";
import * as Icons from "react-native-heroicons/solid";
import { heightPercentageToDP as hp } from "react-native-responsive-screen";
import Button from "../components/button";
import { colors } from "../theme";
import { changePassword } from "../controller/ChangePasswordController";
import Toast from "react-native-toast-message";

const width = Dimensions.get("window").width;

const EditScreen = () => {
    const navigation = useNavigation();
    const [oldPassword, setOldPassword] = useState("");
    const [newPassword, setNewPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const menuItems = [
        { icon: "BookmarkIcon", title: "Đã lưu" },
        { icon: "ClipBoardDocumentListIcon", title: "Đơn hàng" },
        { icon: "HeartIcon", title: "Yêu thích" },
    ];

    const handleChangePassword = async () => {
        if (oldPassword == "" || newPassword == "" || confirmPassword == "") {
            Toast.show({
                type: "error",
                text1: "Thông báo",
                text2: "Vui lòng nhập đầy đủ thông tin",
                topOffset: 70,
                text1Style: { fontSize: 18 },
                text2Style: { fontSize: 15 },
            });
            return;
        }
        const rs = await changePassword(oldPassword, newPassword, confirmPassword);
        Toast.show({
            type: rs[0] ? "success" : "error",
            text1: 'Thông báo',
            text2: rs[1],
            topOffset: 70,
            duration: 2000,
            text1Style: { fontSize: 18 },
            text2Style: { fontSize: 15 },
        })

        if (rs[0]) {
            setTimeout(() => {
                navigation.replace('Login')
            }, 2000);
        }
    }

    return (
        <View className="flex-1">
            <ScrollView>
                {/* avatar */}
                <View style={{ height: hp(25) }} className="bg-yellow-950">
                    <View className="justify-center items-center mt-20 mx-5">
                        <Text className="text-white text-xl font-bold text-center">
                            Đổi mật khẩu
                        </Text>

                        <TouchableOpacity
                            onPress={() => navigation.goBack()}
                            style={{ position: "absolute", left: 0 }}
                        >
                            <Icons.ChevronLeftIcon size={30} color="#ffffff" />
                        </TouchableOpacity>
                    </View>
                    <View className="absolute bottom-5 mx-5">
                        <Text className="text-white text-base font-semibold text-center">
                            Thay đổi mật khẩu thường xuyên để bảo vệ tài khoản
                            của bạn
                        </Text>
                    </View>
                </View>

                {/* edit password */}
                <View className="mx-5">
                    <View className="space-y-1 mt-5">
                        <Text
                            className="font-semibold text-base"
                            style={{ color: colors.text(1) }}
                        >
                            Mật khẩu cũ
                        </Text>

                        <View
                            className="border rounded-lg"
                            style={{ borderColor: "#9d9d9d" }}
                        >
                            <TextInput
                                value={oldPassword}
                                className="p-3 text-base"
                                placeholder="Mật khẩu cũ"
                                onChangeText={(e) => setOldPassword(e)}
                            />
                        </View>
                    </View>

                    <View className="space-y-1 mt-5">
                        <Text
                            className="font-semibold text-base"
                            style={{ color: colors.text(1) }}
                        >
                            Mật khẩu mới
                        </Text>

                        <View
                            className="border rounded-lg"
                            style={{ borderColor: "#9d9d9d" }}
                        >
                            <TextInput
                                value={newPassword}
                                className="p-3 text-base"
                                placeholder="Mật khẩu mới"
                                onChangeText={(e) => setNewPassword(e)}
                            />
                        </View>
                    </View>

                    <View className="space-y-1 mt-5">
                        <Text
                            className="font-semibold text-base"
                            style={{ color: colors.text(1) }}
                        >
                            Xác nhận mật khẩu
                        </Text>

                        <View
                            className="border rounded-lg"
                            style={{ borderColor: "#9d9d9d" }}
                        >
                            <TextInput
                                value={confirmPassword}
                                className="p-3 text-base"
                                placeholder="Xác nhận mật khẩu"
                                onChangeText={(e) => setConfirmPassword(e)}
                            />
                        </View>
                    </View>
                </View>

                {/* button */}
                <View className="mt-10 mx-5">
                    <Button content="Cập nhật mật khẩu" handle={handleChangePassword}/>
                </View>
            </ScrollView>
        </View>
    );
};

export default EditScreen;
