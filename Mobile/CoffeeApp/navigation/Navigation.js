import { View, Text } from 'react-native'
import React from 'react'
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import HomeScreen from '../screens/HomeScreen';
import WelcomeScreen from '../screens/WelcomeScreen';
import LogInScreen from '../screens/LogInScreen';
import RegisterScreen from '../screens/RegisterScreen';
import ForgotScreen from '../screens/ForgotScreen';
import VerifyScreen from '../screens/VerifyScreen';
import DetailItemScreen from '../screens/DetailItemScreen';
import BottomTab from './BottomTab';
import EditScreen from '../screens/EditScreen';
import ChangePassword from '../screens/ChangePassword';
import CartScreen from '../screens/CartScreen';
import AddressScreen from '../screens/AddressScreen';
import AddAddressScreen from '../screens/AddAddressScreen';
import MapScreen from '../screens/MapScreen';
import PreparePayScreen from '../screens/PreparePayScreen';
import OrderSuccessScreen from '../screens/OrderSuccessScreen';
import OrderInfoScreen from '../screens/OrderInfoScreen';

const Stack = createNativeStackNavigator();

export default function Navigation() {
  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName='Login' screenOptions={{headerShown: false}}>
        <Stack.Screen name="HomeTab" component={BottomTab} />
        <Stack.Screen name="Welcome" component={WelcomeScreen} />
        <Stack.Screen name="Login" component={LogInScreen} />
        <Stack.Screen name="Register" component={RegisterScreen} />
        <Stack.Screen name="Forgot" component={ForgotScreen} />
        <Stack.Screen name="Verify" component={VerifyScreen} />
        <Stack.Screen name="Detail" component={DetailItemScreen} />
        <Stack.Screen name="Edit" component={EditScreen} />
        <Stack.Screen name="ChangePassword" component={ChangePassword} />
        <Stack.Screen name="Cart" component={CartScreen} />
        <Stack.Screen name="Address" component={AddressScreen} />
        <Stack.Screen name="AddAddress" component={AddAddressScreen} />
        <Stack.Screen name="Prepare" component={PreparePayScreen} />
        <Stack.Screen name="OrderSuccess" component={OrderSuccessScreen} />
        <Stack.Screen name="OrderInfo" component={OrderInfoScreen} />
        <Stack.Screen name="MapView" component={MapScreen} options={{presentation: 'modal'}}/>
      </Stack.Navigator>
    </NavigationContainer>
  )
}