import React, {useState} from 'react';
import {View, Text, StyleSheet, FlatList} from 'react-native';
import LargeButton from './components/LargeButton';

export default Reports = (props, {navigation, route}) => {
  return (
    <View style={styles.container}>
      <View style={styles.title}>
        <Text style={styles.header}>Distillation</Text>
        <Text style={styles.text}>Reports</Text>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    margin: '10%',
  },
  title: {
    alignItems: 'center',
    justifyContent: 'center',
  },
  header: {
    fontSize: 84,
    fontStyle: 'italic',
    fontWeight: 'bold',
  },
  text: {
    fontSize: 62,
  },
  buttons: {
    marginTop: '15%',
    flex: 1,
    flexDirection: 'column',
  },
});
