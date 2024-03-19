import { View, Text, Dimensions, TouchableOpacity, Image, TextInput } from 'react-native'
import React from 'react'
import { widthPercentageToDP as wp, heightPercentageToDP as hp } from "react-native-responsive-screen";
import Button from "../components/button";
import { colors } from "../theme";
import { useNavigation } from '@react-navigation/native';

const { width, height } = Dimensions.get("window");


const RegisterScreen = () => {
    const navigation = useNavigation();
  return (
    <View className="flex-1 justify-center items-center">
            <Image
                className="relative"
                source={require("../assets/images/bgOther.png")}
                style={{ width: width, height: height }}
            />
            <View className="absolute top-36">
                <Text className="text-4xl font-extrabold text-center mb-20" style-={{ color: colors.primary }}>
                    Chào bạn mới!
                </Text>
                <View className="space-y-3" style={{ width: wp(90) }}>
                    <TextInput placeholder="Tên đăng nhập" className='p-4 border-[1px] rounded-lg text-lg leading-5' style={{borderColor: 'rgba(59, 29, 12, 0.4)'}}/>
                    <TextInput placeholder="Email" secureTextEntry={true} className="p-4 border-[1px] rounded-lg leading-5 text-base" style={{borderColor: 'rgba(59, 29, 12, 0.4)'}}/>
                    <TextInput placeholder="Mật khẩu" secureTextEntry={true} className="p-4 border-[1px] rounded-lg leading-5 text-base" style={{borderColor: 'rgba(59, 29, 12, 0.4)'}}/>
                    <TextInput placeholder="Nhập lại mật khẩu" secureTextEntry={true} className="mb-3 p-4 border-[1px] rounded-lg leading-5 text-base" style={{borderColor: 'rgba(59, 29, 12, 0.4)'}}/>
                    <Button content='Đăng ký' handle={() => {}} />
                </View>

                <View className='mt-10 flex-row justify-between items-center' style={{width: wp(90)}}>
                    <Text style={{height: 1, borderColor: 'rgba(59, 29, 12, 0.4)', borderWidth: 1, width: wp(25)}}></Text>

                    <Text style={{color: '#8B6122'}}>Hoặc đăng ký với</Text>

                    <Text style={{height: 1, borderColor: 'rgba(59, 29, 12, 0.4)', borderWidth: 1, width: wp(25)}}></Text>
                </View>

                <View className='flex justify-center items-center bg-white border-[1px] rounded-lg py-1 mt-5' style={{borderColor: '#E8ECF4'}}>
                    <Image source={require('../assets/icons/ggIcon.png')}/>
                </View>

                <View className='flex flex-row justify-center items-center mt-10'>
                    <Text className='font-semibold'>Đã có tài khoản? </Text>
                    <TouchableOpacity onPress={() => navigation.goBack()}>
                        <Text className='font-semibold' style={{color: colors.active}}>Đăng nhập tại đây!</Text>
                    </TouchableOpacity>
                </View>
            </View>
        </View>
  )
}

export default RegisterScreen