import { useNavigation } from "@react-navigation/native";
import React from "react";
import { Dimensions, ScrollView, Text, TouchableOpacity, View } from "react-native";
import * as Icons from "react-native-heroicons/solid";
import { heightPercentageToDP as hp } from "react-native-responsive-screen";
import Button from "../components/button";
import { colors } from "../theme";

const width = Dimensions.get("window").width;

const EditScreen = () => {
    const navigation = useNavigation();
    const menuItems = [
        { icon: "BookmarkIcon", title: "Đã lưu" },
        { icon: "ClipBoardDocumentListIcon", title: "Đơn hàng" },
        { icon: "HeartIcon", title: "Yêu thích" },
    ];
    return (
        <View className="flex-1">
            <ScrollView>
                {/* avatar */}
                <View style={{ height: hp(25) }} className="bg-yellow-950">
                    <View className="justify-center items-center mt-20 mx-5">
                        <Text className="text-white text-xl font-bold text-center">Đổi mật khẩu</Text>

                        <TouchableOpacity onPress={() => navigation.goBack()} style={{ position: "absolute", left: 0 }}>
                            <Icons.ChevronLeftIcon size={30} color="#ffffff" />
                        </TouchableOpacity>
                    </View>
                    <View className="absolute bottom-5 mx-5">
                        <Text className="text-white text-base font-semibold text-center">
                            Thay đổi mật khẩu thường xuyên để bảo vệ tài khoản của bạn
                        </Text>
                    </View>
                </View>

                {/* edit password */}
                <View className="mx-5">
                    <View className="space-y-1 mt-5">
                        <Text className="font-semibold text-base" style={{ color: colors.text(1) }}>
                            Mật khẩu cũ
                        </Text>

                        <View className="border rounded-lg" style={{ borderColor: "#9d9d9d" }}>
                            <Text className="p-3 text-base">namdeptrai</Text>
                        </View>
                    </View>

                    <View className="space-y-1 mt-5">
                        <Text className="font-semibold text-base" style={{ color: colors.text(1) }}>
                            Mật khẩu mới
                        </Text>

                        <View className="border rounded-lg" style={{ borderColor: "#9d9d9d" }}>
                            <Text className="p-3 text-base">namdeptrai</Text>
                        </View>
                    </View>

                    <View className="space-y-1 mt-5">
                        <Text className="font-semibold text-base" style={{ color: colors.text(1) }}>
                            Xác nhận mật khẩu
                        </Text>

                        <View className="border rounded-lg" style={{ borderColor: "#9d9d9d" }}>
                            <Text className="p-3 text-base">namdeptrai</Text>
                        </View>
                    </View>
                </View>

                {/* button */}
                <View className="mt-10 mx-5">
                    <Button content="Cập nhật mật khẩu" />
                </View>
            </ScrollView>
        </View>
    );
};

export default EditScreen;
