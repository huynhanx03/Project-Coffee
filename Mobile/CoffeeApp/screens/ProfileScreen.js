import { View, Text, Dimensions, Image, ScrollView, TouchableOpacity, Alert } from "react-native";
import React from "react";
import Carousel from "react-native-reanimated-carousel";
import { widthPercentageToDP as wp, heightPercentageToDP as hp } from "react-native-responsive-screen";
import * as Icons from "react-native-heroicons/solid";
import MenuItemProfile from "../components/menuItemProfile";
import { useNavigation } from "@react-navigation/native";
import Toast from "react-native-toast-message";
import { useDispatch } from "react-redux";
import { clearCart } from "../redux/slices/cartSlice";

const width = Dimensions.get("window").width;

const ProfileScreen = () => {
    const navigation = useNavigation();
    const dispatch = useDispatch();
    const menuItems = [
        { icon: "BookmarkIcon", title: "Đã lưu" },
        { icon: "ClipBoardDocumentListIcon", title: "Đơn hàng" },
        { icon: "HeartIcon", title: "Yêu thích" },
    ];

    const handleLogout = () => {
        Alert.alert(
            "Xác nhận",
            "Bạn có chắc chắn muốn đăng xuất không?",
            [
                { text: "Hủy", style: "cancel" },
                { text: "Đăng xuất", style: 'destructive', onPress: () => {
                    navigation.replace('Login')
                    dispatch(clearCart());
                } },
            ],
            { cancelable: true }
        );
    };

    return (
        <View className="flex-1">
            <ScrollView showsVerticalScrollIndicator={false}>
                {/* avatar */}
                <View style={{ height: hp(25) }} className="bg-yellow-950">
                    <View className="justify-center items-center mt-20">
                        <Text className="text-white text-xl font-bold text-center">Hồ sơ của tôi</Text>
                    </View>
                    <View className="justify-center items-center mt-16">
                        <Image
                            source={require("../assets/images/avtDemo.png")}
                            resizeMode="contain"
                            style={{ width: hp(12), height: hp(12) }}
                        />
                    </View>
                </View>

                {/* info */}
                <View className="mt-16 space-y-1">
                    <View className="flex-row justify-center gap-2 items-center">
                        <Text className="text-lg font-semibold text-center">Ngo Nam</Text>
                        <TouchableOpacity
                            onPress={() => navigation.navigate("Edit")}
                            className="p-1 bg-gray-300 rounded-full">
                            <Icons.PencilIcon size={20} color="#000000" />
                        </TouchableOpacity>
                    </View>
                    <Text className="text-center text-gray-500">namngo102003@gmail.com</Text>
                    <Text className="text-center text-gray-500">0987654321</Text>
                </View>

                {/* menu item */}
                <View className="mx-5 mt-10">
                    {menuItems.map((item, index) => {
                        return <MenuItemProfile key={index} icon={item.icon} title={item.title} />;
                    })}
                </View>

                {/* logout */}
                <View>
                    <TouchableOpacity onPress={handleLogout} className="mx-5 mt-10 p-4 bg-red-500 rounded-lg">
                        <Text className="text-white text-center text-lg font-semibold">Đăng xuất</Text>
                    </TouchableOpacity>
                </View>
            </ScrollView>
        </View>
    );
};

export default ProfileScreen;
