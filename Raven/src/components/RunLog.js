import {TabActions} from '@react-navigation/native';
import React from 'react';
import {StyleSheet, View, Text} from 'react-native';

function Choice() {
  return (
    <Tab.Navitation>
      <Stack.Screen name="MaterialChoice" component={MaterialChoice} />
    </Tab.Navitation>
  );
}

export default function RunLog({navigation}) {
  return <Text> Run Log </Text>;
}
