import { View, Text } from "react-native";
import { Image } from "expo-image";
import {
    heightPercentageToDP as hp,
    widthPercentageToDP as wp,
} from "react-native-responsive-screen";
import React from "react";
import { colors } from "../theme";
import { formatPrice } from "../utils";
import { blurhash } from "../utils";
import { Divider } from "react-native-paper";

const ItemPay = (props) => {
    return (
        <View className="mx-1 rounded-lg mb-2">
            <View className="flex-row space-x-5 items-center p-2">
                <Image
                    source={{ uri: props.item.HinhAnh }}
                    style={{
                        borderRadius: 10,
                        width: wp(20),
                        height: wp(20),
                        backgroundColor: 'white'
                    }}
                    contentFit="contain"
                    placeholder={{ blurhash }}
                    transition={1000}
                />
                <View className="flex-1 space-y-1">
                    <Text className="text-lg font-semibold">
                        {props.item.TenSanPham}
                    </Text>

                    <View className="flex-row">
                        <Text className="text-base text-gray-700">Size: </Text>
                        <Text className="text-base text-gray-700">
                            {props.item.KichThuoc}
                        </Text>
                    </View>

                    <View className="flex-row justify-between">
                        <Text className="text-base text-gray-700">
                            {formatPrice(props.item.Gia)}
                        </Text>
                        <Text className="text-base text-gray-700">
                            x{props.item.SoLuong}
                        </Text>
                    </View>
                </View>
            </View>
            <Divider />
        </View>
    );
};

export default ItemPay;
