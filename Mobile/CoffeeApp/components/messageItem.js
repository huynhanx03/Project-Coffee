import { View, Text, Image } from "react-native";
import {
    heightPercentageToDP as hp,
    widthPercentageToDP as wp,
} from "react-native-responsive-screen";
import React, { useEffect } from "react";

const MessageItem = ({ message, currentUser }) => {
    if (message.NoiDung) {
        if (currentUser?.MaNguoiDung === message.MaKH) {
            return (
                <View className="flex-row justify-end mb-2 items-center gap-2">
                    <View style={{ width: wp(80) }}>
                        <View className="flex self-end p-3 rounded-2xl bg-white border border-neutral-200">
                            <Text style={{ fontSize: hp(1.9) }}>{message.NoiDung}</Text>
                        </View>
                        <Text className='italic text-gray-400' style={{alignSelf: 'flex-end'}}>{message.ThoiGian}</Text>
                    </View>
                </View>
            );
        } else {
            return (
                <View style={{width: wp(80)}} className='mb-2'>
                    <View className="flex self-start p-3 rounded-2xl bg-amber-400 border border-indigo-200">
                        <Text style={{fontSize: hp(1.9)}}>{message.NoiDung}</Text>
                    </View>
                    <Text className='italic text-gray-400' style={{alignSelf: 'flex-start'}}>{message.ThoiGian}</Text>
                </View>
            )
        }
    } else {
        return (
            null
        )
    }
};

export default MessageItem;
