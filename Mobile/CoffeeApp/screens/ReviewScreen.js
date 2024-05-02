import { View, Text, TextInput, TouchableOpacity } from "react-native";
import React from "react";
import {
    heightPercentageToDP as hp,
    widthPercentageToDP as wp,
} from "react-native-responsive-screen";

const ReviewScreen = () => {
    return (
        <View className="flex-1 p-2">
            <View>
                
            </View>

            <View className="p-1 border border-gray-300 rounded-lg">
                <TextInput
                    multiline={true}
                    placeholder="Hãy cho chúng tôi biết bạn cảm nhận thế nào về coffee của chúng tôi nhé!"
                    className="p-2 text-base"
                    style={{height: hp(20)}}
                />
            </View>

            <TouchableOpacity className='bg-amber-300 flex-row justify-center mt-5 rounded-md'>
                <Text className='p-2 text-base font-semibold py-3'>Gửi nhận xét</Text>
            </TouchableOpacity>
        </View>
    );
};

export default ReviewScreen;
