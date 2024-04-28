import { View, Text, Image } from "react-native";
import React, { useEffect } from "react";
import { useNavigation } from "@react-navigation/native";
import { colors } from "../theme";
import {widthPercentageToDP as wp, heightPercentageToDP as hp} from 'react-native-responsive-screen';
import Animated, { useSharedValue, withSpring } from 'react-native-reanimated';

export default function WelcomeScreen() {
    const navigation = useNavigation();
    const ring1padding = useSharedValue(0);
    const ring2padding = useSharedValue(0);
    const ring3padding = useSharedValue(0);

    useEffect(() => {
        ring1padding.value = 0;
        ring2padding.value = 0;
        ring3padding.value = hp(4);

        setTimeout(() => {
            ring1padding.value = withSpring(ring1padding.value + hp(3), {damping: 100});
        }, 100);

        setTimeout(() => {
            ring2padding.value = withSpring(ring2padding.value + hp(3));
        }, 100);

        setTimeout(() => {
            ring3padding.value = withSpring(ring3padding.value + hp(46));
        }, 1000);
    })

    useEffect(() => {
        setTimeout(() => {
            navigation.replace("Login");
        }, 3000);
    });
    return (
        <View className="flex-1 justify-center items-center" style={{backgroundColor: colors.primary}}>
            <Animated.View className='bg-white/10 rounded-full' style={{padding: ring2padding}}>
                <Animated.View className='bg-white/20 rounded-full ' style={{padding: ring1padding}}>
                    <Animated.View style={{padding: ring3padding}} className='rounded-full bg-white'>
                        <Image source={require('../assets/images/logo.png')} resizeMode="contain" style={{width: hp(15), height: hp(15)}}/>
                    </Animated.View>
                </Animated.View>
            </Animated.View>
        </View>
    );
}
