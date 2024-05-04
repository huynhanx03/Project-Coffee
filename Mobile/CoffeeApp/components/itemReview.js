import { View, Text, Image } from "react-native";
import React, { useEffect, useState } from "react";
import {
    heightPercentageToDP as hp,
    widthPercentageToDP as wp,
} from "react-native-responsive-screen";
import { Divider } from "react-native-paper";
import { getUserById } from "../controller/UserController";
import { Rating } from "react-native-ratings";

const ItemReview = (props) => {
    const [user, setUser] = useState({});
    const review = props.review;
    const handleGetUser = async () => {
        try {
            const user = await getUserById(review.MaNguoiDung);
            setUser(user);
        } catch (error) {
            console.log(error);
        }
    };
    useEffect(() => {
        handleGetUser();
    }, []);
    return (
        <View className="mt-5">
            <View className="flex-row justify-between items-center">
                <View className="flex-row items-center space-x-3">
                    <Image
                        source={{ uri: user?.HinhAnh }}
                        style={{ width: wp(15), height: wp(15) }}
                        className="rounded-full"
                    />
                    <View style={{ width: wp(55) }}>
                        <Text className="text-base font-bold">
                            {user?.HoTen}
                        </Text>
                        <Text>{review?.VanBanDanhGia}</Text>
                    </View>
                </View>
                <View>
                    <Rating
                        tintColor="#f2f2f2"
                        readonly
                        startingValue={review.DiemDanhGia}
                        imageSize={15}
                        type="star"
                        style={{ alignItems: "flex-start", marginBottom: 5}}
                    />
                    <Text>{review?.ThoiGianDanhGia.split(" ")[1]}</Text>
                </View>
            </View>
            <Divider className="mt-4" />
        </View>
    );
};

export default ItemReview;
