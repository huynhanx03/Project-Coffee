import { View, Text, TextInput, TouchableOpacity } from "react-native";
import React, { useEffect, useState } from "react";
import {
    heightPercentageToDP as hp,
    widthPercentageToDP as wp,
} from "react-native-responsive-screen";
import { AntDesign } from "@expo/vector-icons";
import { setReview } from "../controller/ReviewController";
import Toast from "react-native-toast-message";
import { useNavigation } from "@react-navigation/native";
import { Rating, AirbnbRating } from "react-native-ratings";
import ShowToast from "../components/toast";

const ReviewScreen = ({route}) => {
    const navigation = useNavigation()
    const star = {
        fill: <AntDesign name="star" size={24} color="orange" />,
        noFill: <AntDesign name="staro" size={24} color="black" />,
    };

    const [rating, setRating] = useState(0);
    const [reviewContent, setReviewContent] = useState("");

    const ratings = [1, 2, 3, 4, 5];

    const handleSendReview = async () => {
        const tx = await setReview(route.params.MaSanPham, rating, reviewContent)
        navigation.goBack()
        if (tx) {
            ShowToast("success", "Gửi nhận xét thành công", "Cảm ơn bạn đã góp ý cho chúng tôi!")
        } else {
            ShowToast("error", "Gửi nhận xét thất bại", "Vui lòng thử lại sau!")
        }
    }
    return (
        <View className="flex-1 p-2 space-y-5">
            <View>
                <Text className="text-lg font-semibold text-center">
                    Nhận xét sản phẩm
                </Text>
            </View>

            {/* star */}
            <View className="flex-row space-x-1 items-center">
                <Text className='text-base'>Cafe của chúng mình được: </Text>
                <Rating ratingCount={5}
                        tintColor="#f2f2f2"
                        jumpValue={1}  
                        startingValue={0}
                        type="star"
                        ratingTextColor="red"
                        onFinishRating={(rating) => setRating(rating)}
                        imageSize={wp(8)}
                />
            </View>

            <View className="p-1 border border-gray-300 rounded-lg">
                <TextInput
                    value={reviewContent}
                    onChangeText={(e) => setReviewContent(e)}
                    multiline={true}
                    placeholder="Hãy cho chúng mình biết bạn cảm nhận thế nào về cafe nhé!"
                    className="p-2 text-base"
                    style={{ height: hp(20) }}
                />
            </View>

            <TouchableOpacity onPress={handleSendReview} className="bg-amber-500 flex-row justify-center mt-5 rounded-md">
                <Text className="p-2 text-base font-semibold py-3">
                    Gửi nhận xét
                </Text>
            </TouchableOpacity>
        </View>
    );
};

export default ReviewScreen;
