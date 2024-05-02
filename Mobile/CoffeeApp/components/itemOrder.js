import { View, Text, Image } from "react-native";
import React from "react";
import {
    heightPercentageToDP as hp,
    widthPercentageToDP as wp,
} from "react-native-responsive-screen";
import { formatPrice } from "../utils";
import { Divider } from "react-native-paper";
import { colors } from "../theme";
import * as Icons from "react-native-heroicons/outline";

const ItemOrder = () => {
    return (
        <View className="flex-1 space-y-2 bg-orange-100 rounded-md mt-2">
            <View className="space-y-5 mx-2">
                <View className="flex-row justify-between">
                    <Text className="text-base">Trạng thái</Text>
                    <Text
                        className="text-base font-semibold"
                        style={{ color: colors.text(1) }}
                    >
                        Hoàn thành
                    </Text>
                </View>

                <View className="flex-row space-x-5">
                    <Image
                        source={require("../assets/images/coffeeDemo.png")}
                        style={{ width: wp(20), height: wp(22) }}
                        className="rounded-lg"
                    />

                    <View className="flex-1 space-y-2">
                        <Text className="text-lg font-semibold">
                            Cà phê sữa
                        </Text>
                        <Text className="text-base">Size: M</Text>
                        <View className='flex-row justify-between'>
                            <Text className='text-base'>{formatPrice(200000)}</Text>
                            <Text className="text-base">x1</Text>
                        </View>
                    </View>
                </View>
            </View>

            <Divider className='bg-gray-400 p-[0.2px]'/>

            <View className="flex-row justify-end mx-2">
                <Text className="text-base">Thành tiền: </Text>
                <Text
                    style={{ color: colors.text(1) }}
                    className="text-base font-semibold"
                >
                    {formatPrice(200000)}
                </Text>
            </View>

            <Divider className='bg-gray-400 p-[0.2px]'/>

            <View className="mx-2 flex-row items-center space-x-2 pb-2">
                <Icons.TruckIcon size={30} color={colors.active} />
                <Text className="text-base">
                    Đơn hàng đã được giao thành công
                </Text>
            </View>

            {/* <Divider className="" style={{ backgroundColor: "#f2f2f2" }} /> */}
        </View>
    );
};

export default ItemOrder;
