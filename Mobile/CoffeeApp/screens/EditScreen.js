import { useNavigation } from "@react-navigation/native";
import * as ImagePicker from "expo-image-picker";
import React, { useEffect, useState } from "react";
import { Image } from "expo-image"
import {
    Dimensions,
    Pressable,
    ScrollView,
    Text,
    TouchableOpacity,
    View,
} from "react-native";
import * as Icons from "react-native-heroicons/solid";
import {
    heightPercentageToDP as hp,
    widthPercentageToDP as wp,
} from "react-native-responsive-screen";
import InputCustom from "../components/inputCustom";
import MenuItemProfile from "../components/menuItemProfile";
import { getUserData } from "../controller/StorageController";
import { uploadImage } from "../controller/UploadAvatarController";
import { blurhash } from "../utils";
import ShowToast from "../components/toast";

const width = Dimensions.get("window").width;

const EditScreen = () => {
    const navigation = useNavigation();
    const [user, setUser] = useState(null);

    const handleClick = () => {
        navigation.navigate("ChangePassword");
    };

    const getUser = async () => {
        try {
            const user = await getUserData();
            setUser(user);
        } catch (error) {
            console.log(error);
        }
    };

    useEffect(() => {
        getUser();
    }, []);

    const handleUploadImage = (image) => {
        const data = new FormData();
        data.append("file", image);
        data.append("upload_preset", "CoffeeAvatar");
        data.append("cloud_name", "dev9hnuhw");

        fetch("https://api.cloudinary.com/v1_1/dev9hnuhw/image/upload", {
            method: "post",
            body: data,
        })
            .then((res) => res.json())
            .then(async (data) => {
                const rs = await uploadImage(data.secure_url);
                ShowToast(rs[0] ? "success" : "error", rs[1], rs[0] ? "Vui lòng đăng nhập lại" : "Vui lòng thử lại");
                if (rs[0]) {
                    navigation.popToTop();
                    navigation.replace("Login");
                }
            });
    };

    const [image, setImage] = useState(null);

    const pickImage = async () => {
        // No permissions request is necessary for launching the image library
        let result = await ImagePicker.launchImageLibraryAsync({
            mediaTypes: ImagePicker.MediaTypeOptions.All,
            allowsEditing: true,
            aspect: [1, 1],
            quality: 1,
        });

        // console.log(result.assets[0].uri);

        if (!result.canceled) {
            // setImage(result.assets[0].uri);
            let newFile = {
                uri: result.assets[0].uri,
                type: `test/${result.assets[0].uri.split(".")[1]}`,
                name: `test.${result.assets[0].uri.split(".")[1]}`,
            };
            handleUploadImage(newFile);
        }
    };

    return (
        <View className="flex-1">
            <ScrollView showsVerticalScrollIndicator={false}>
                {/* avatar */}
                <View style={{ height: hp(25) }} className="bg-yellow-950">
                    <View
                        className="justify-center items-center"
                        style={{ marginTop: hp(9) }}
                    >
                        <Text className="text-white text-xl font-bold text-center">
                            Chỉnh sửa hồ sơ
                        </Text>

                        <Pressable
                            onPress={() => navigation.goBack()}
                            style={{ position: "absolute", left: wp(5) }}
                        >
                            <Icons.ChevronLeftIcon size={24} color="white" />
                        </Pressable>
                    </View>
                    <View
                        className="justify-center items-center"
                        style={{ marginTop: hp(6) }}
                    >
                        <Image
                            source={{
                                uri: user?.HinhAnh
                                    ? user?.HinhAnh
                                    : "https://user-images.githubusercontent.com/5709133/50445980-88299a80-0912-11e9-962a-6fd92fd18027.png",
                            }}
                            contentFit="cover"
                            placeholder={{ blurhash }}
                            style={{ width: hp(12), height: hp(12) }}
                            className="rounded-full"
                            transition={1000}
                        />
                    </View>
                </View>

                {/* edit avatar */}
                <View className="mt-16 items-center">
                    <TouchableOpacity
                        onPress={pickImage}
                        className="p-3 bg-yellow-950 rounded-lg"
                    >
                        <Text className="text-white font-semibold">
                            Đổi ảnh đại diện
                        </Text>
                    </TouchableOpacity>
                </View>

                {/* Account info */}
                <View className="mt-5 p-2 px-5 bg-slate-200">
                    <Text
                        className="text-base font-semibold"
                        style={{ color: "gray" }}
                    >
                        Thông tin tài khoản
                    </Text>
                </View>
                <View className="mx-5 mt-1">
                    <InputCustom label={"Họ và tên"} content={user?.HoTen} isEdit={true} />
                    <InputCustom label={"Giới tính"} content={user?.GioiTinh} isEdit={true} />
                    <InputCustom label={"Ngày sinh"} content={user?.NgaySinh} isEdit={true} />
                    <InputCustom
                        label={"Tên đăng nhập"}
                        content={user?.TaiKhoan}
                        isEdit={false}
                    />
                    <InputCustom label={"Email"} content={user?.Email} isEdit={false}/>
                    <InputCustom
                        label={"Số điện thoại"}
                        content={user?.SoDienThoai}
                        isEdit={false}
                    />
                    <InputCustom
                        label={"Ngày tham gia"}
                        content={user?.NgayTao}
                        isEdit={false}
                    />

                    <MenuItemProfile
                        icon="Key"
                        title="Đổi mật khẩu"
                        handleClick={handleClick}
                    />
                </View>

                <View className="mt-10"></View>
            </ScrollView>
        </View>
    );
};

export default EditScreen;
