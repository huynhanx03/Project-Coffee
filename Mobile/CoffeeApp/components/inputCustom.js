import { View, Text, TextInput } from 'react-native'
import React from 'react'
import { colors } from '../theme'

const InputCustom = ({lable, icon}) => {
  return (
    <View className='space-y-1 mt-5'>
      <Text className='font-semibold text-base' style={{color: colors.text(1)}}>{lable}</Text>

      <View className='border rounded-lg' style={{borderColor: '#9d9d9d'}}>
        <Text className='p-3 text-base'>namdeptrai</Text>
      </View>
    </View>
  )
}

export default InputCustom