import { View, Text, Image, TouchableOpacity } from "react-native";
import React, { useEffect, useMemo, useState } from "react";
import {
    heightPercentageToDP as hp,
    widthPercentageToDP as wp,
} from "react-native-responsive-screen";
import { formatPrice } from "../utils";
import { Divider } from "react-native-paper";
import { colors } from "../theme";
import * as Icons from "react-native-heroicons/outline";
import { Ionicons } from "@expo/vector-icons";
import { useNavigation } from "@react-navigation/native";

const ItemOrder = (props) => {
    const navigation = useNavigation()
    const [quantity, setQuantity] = useState(0);
    const [isReview, setIsReview] = useState(true);
    const calQuantity = useMemo(() => {
        let total = 0;
        props.order.SanPham.forEach((item) => {
            total += item.SoLuongGioHang;
        })

        setQuantity(total);
    }, [])

    const handleReview = () => {
        setIsReview(false);
    }

    return (
        <View className="flex-1">
            <View className="space-y-3 rounded-md mt-4 bg-white shadow-md">
                <View className="space-y-5 mx-2">
                    <View className="flex-row justify-between p-1">
                        <Text
                            className="text-base font-bold"
                            style={{ color: colors.text(1) }}
                        >
                            {props.order.MaDonHang}
                        </Text>
                        <View className="flex-row justify-end">
                            <Text className="text-base">Trạng thái: </Text>
                            <Text
                                className="text-base font-semibold"
                                style={{ color: colors.text(1) }}
                            >
                                {props.order.TrangThai}
                            </Text>
                        </View>
                    </View>

                    {props.order.SanPham.map((item, index) => {
                        return (
                            <View className="flex-row space-x-5">
                                <Image
                                    source={require("../assets/images/coffeeDemo.png")}
                                    style={{
                                        width: wp(20),
                                        height: wp(22),
                                    }}
                                    className="rounded-lg"
                                />

                                <View className="flex-1 space-y-2">
                                    <Text className="text-lg font-semibold">
                                        {item.TenSanPham}
                                    </Text>
                                    <Text className="text-base">
                                        Size: {item.KichThuoc}
                                    </Text>
                                    <View className="flex-row justify-between">
                                        <Text className="text-base">
                                            {formatPrice(item.Gia)}
                                        </Text>
                                        <Text className="text-base">
                                            x{item.SoLuongGioHang}
                                        </Text>
                                    </View>
                                </View>
                            </View>
                        );
                    })}
                </View>

                <Divider className="bg-gray-400 p-[0.2px]" />

                <View className="flex-row items-center justify-between">
                    <View className='flex-row mx-2'>
                        <Ionicons
                            name="fast-food-outline"
                            size={24}
                            color={'gray'}
                        />
                        <Text className="mx-2 text-base text-gray-500 font-semibold">
                            {quantity} sản phẩm
                        </Text>
                    </View>
                    <View className="flex-row justify-end mx-2">
                        <Text className="text-base">Thành tiền: </Text>
                        <Text
                            style={{ color: colors.text(1) }}
                            className="text-base font-semibold"
                        >
                            {formatPrice(props.order.ThanhTien)}
                        </Text>
                    </View>
                </View>

                <Divider className="bg-gray-400 p-[0.2px]" />

                <View className="mx-2 flex-row items-center space-x-2">
                    <Icons.TruckIcon size={30} color={colors.active} />
                    <Text className="text-base">
                        Đơn hàng đang trong quá trình xác nhận
                    </Text>
                </View>

                <Divider className="bg-gray-400 p-[0.2px]" />

                <View className='flex-row justify-between mx-2 pb-2'>
                    <TouchableOpacity onPress={handleReview} disabled={!isReview} className='p-3 rounded-lg' style={{backgroundColor: isReview ? colors.active : '#d2d2d2'}}>
                        <Text className='text-base font-semibold'>Đã nhận được hàng</Text>
                    </TouchableOpacity>

                    <TouchableOpacity onPress={() => navigation.navigate('Review')} disabled={isReview} className='p-3 rounded-lg' style={{backgroundColor: isReview ? '#d2d2d2' : colors.active}}>
                        <Text className='text-base font-semibold'>Đánh giá sản phẩm</Text>
                    </TouchableOpacity>
                </View>
            </View>
        </View>
    );
};

export default ItemOrder;
