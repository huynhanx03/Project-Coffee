import { View, Text } from "react-native";
import React, { useEffect, useState } from "react";
import ItemReview from "./itemReview";
import { getReview } from "../controller/ReviewController";

const ItemReviewList = (props) => {
    const reviewList = props.reviewList
    return (
        <View className=''>
            { reviewList && reviewList.map((item) => {
                return (
                    <ItemReview key={item.MaDanhGia} review={item}/>
                )
            }) }

            <View className='p-5'></View>
        </View>
    );
};

export default ItemReviewList;
