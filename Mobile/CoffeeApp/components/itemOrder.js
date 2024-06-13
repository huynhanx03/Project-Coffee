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
import Animated, { FadeOut, FlipInXUp } from "react-native-reanimated";
import { setStatusOrder } from "../controller/OrderController";
import ShowToast from "./toast";

const ItemOrder = (props) => {
    const navigation = useNavigation();
    const [quantity, setQuantity] = useState(0);
    const [isReview, setIsReview] = useState(true);
    const [isReceive, setIsReceive] = useState(false);
    const [status, setStatus] = useState('');
    const orderItemsList = []
    for (const key in props.order.SanPham) {
        orderItemsList.push(props.order.SanPham[key]);
    }
    const calQuantity = useMemo(() => {
        let total = 0;
        orderItemsList.forEach((item) => {
            total += item.SoLuong;
        });

        setQuantity(total);
    }, []);

    const handleReview = () => {
        const MaDonHang = props.order.MaDonHang;
        setStatusOrder(MaDonHang);
        setIsReceive(true);
        setIsReview(false);
        handleStatusDelivery('Đã nhận hàng');
        ShowToast('success', 'Đã nhận đơn hàng', 'Cảm ơn bạn đã mua hàng tại cửa hàng chúng tôi')
    };

    const handleReceive = () => {
        const status = props.order.TrangThai
        setIsReceive(status === 'Đã xác nhận' ? false : true);
    }

    const handleStatus = () => {
        const status = props.order.TrangThai
        setIsReview(status === 'Đã nhận hàng' ? false : true)
    }

    const handleStatusDelivery = (status) => {
        // const status = props.order.TrangThai
        
        if (status === 'Chờ xác nhận') {
            setStatus('Đơn hàng đang trong quá trình xác nhận')
        } else if (status === 'Đã xác nhận') {
            setStatus('Đơn hàng đang trong quá trình vận chuyển')
        } else {
            setStatus('Đơn hàng đã được giao thành công')
        }
    }
    
    useEffect(() => {
        handleStatusDelivery(props.order.TrangThai);
        handleStatus();
        handleReceive()
    }, [])

    return (
        <View className="flex-1">
            <View className="mt-5 flex-row justify-between items-center">
                <Text
                    style={{
                        height: 1,
                        borderColor: "rgba(59, 29, 12, 0.4)",
                        borderWidth: 1,
                        width: wp(25),
                    }}
                ></Text>

                <Text
                    style={{ color: "#8B6122" }}
                    className="text-lg font-semibold"
                >
                    Đơn hàng {props.order.MaDonHang}
                </Text>

                <Text
                    style={{
                        height: 1,
                        borderColor: "rgba(59, 29, 12, 0.4)",
                        borderWidth: 1,
                        width: wp(25),
                    }}
                ></Text>
            </View>
            {orderItemsList.map((item) => {
                return (
                    <Animated.View
                        entering={FlipInXUp}
                        exiting={FadeOut}
                        key={item.MaSanPham}
                        className="flex-1 mt-4"
                    >
                        <View className="space-y-3 rounded-md pt-2 bg-white shadow-md">
                            <View className="space-y-5 mx-2">
                                <View className="flex-row justify-between p-1">
                                    <Text
                                        className="text-base font-bold"
                                        style={{ color: colors.text(1) }}
                                    >
                                        {props.order.MaDonHang}
                                    </Text>
                                    <View className="flex-row justify-end">
                                        <Text className="text-base">
                                            Trạng thái:{" "}
                                        </Text>
                                        <Text
                                            className="text-base font-semibold"
                                            style={{ color: colors.text(1) }}
                                        >
                                            {props.order.TrangThai}
                                        </Text>
                                    </View>
                                </View>

                                <View
                                    key={item.MaSanPham}
                                    className="flex-row space-x-5"
                                >
                                    <Image
                                        source={{ uri: item.HinhAnh }}
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
                                                x{item.SoLuong}
                                            </Text>
                                        </View>
                                    </View>
                                </View>
                            </View>

                            <Divider className="bg-gray-400 p-[0.2px]" />

                            <View className="flex-row items-center justify-between">
                                <View className="flex-row mx-2">
                                    <Ionicons
                                        name="fast-food-outline"
                                        size={24}
                                        color={"gray"}
                                    />
                                    <Text className="mx-2 text-base text-gray-500 font-semibold">
                                        {item.SoLuong} sản phẩm
                                    </Text>
                                </View>
                                <View className="flex-row justify-end mx-2">
                                    <Text className="text-base">
                                        Thành tiền:{" "}
                                    </Text>
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
                                <Icons.TruckIcon
                                    size={30}
                                    color={colors.active}
                                />
                                <Text className="text-base">
                                    {status}
                                </Text>
                            </View>

                            <Divider className="bg-gray-400 p-[0.2px]" />

                            <View className="flex-1 mx-2 pb-2">
                                <TouchableOpacity
                                    onPress={() =>
                                        navigation.navigate("Review", {MaSanPham: item.MaSanPham})
                                    }
                                    disabled={isReview}
                                    className="p-3 rounded-lg"
                                    style={{
                                        backgroundColor: isReview
                                            ? "#d2d2d2"
                                            : colors.active,
                                    }}
                                >
                                    <Text className="text-base font-semibold text-center">
                                        Đánh giá sản phẩm
                                    </Text>
                                </TouchableOpacity>
                            </View>
                        </View>
                    </Animated.View>
                );
            })}

            <TouchableOpacity disabled={isReceive} onPress={handleReview} className='rounded-lg p-3 mt-3 mx-2' style={{backgroundColor: isReview ? colors.active : '#d2d2d2'}}>
                <Text className='text-base font-semibold text-center'>Đã nhận được đơn hàng</Text>
            </TouchableOpacity>
        </View>
    );
};

export default ItemOrder;
