import React, {useState, useEffect} from 'react';
import {StyleSheet, View, Text} from 'react-native';
import Buttons from '../Buttons';

export default function HomeScreen({navigation, route}) {
  const titles = ['Raw Material', 'Run Log', 'Sample', 'Reports'];
  const listtitle = titles.map((title, index) => (
    <Buttons key={index} name={title} />
  ));

  return (
    <View style={styles.page}>
      <View style={styles.headerContainer}>
        <Text style={styles.headerText}>EMD Electronics</Text>
        <Text style={styles.text}>Distillation</Text>
        <View style={styles.container}>{listtitle}</View>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  headerContainer: {
    alignItems: 'center',
  },
  headerText: {
    marginTop: 100,
    fontSize: 45,
  },
  text: {
    alignItems: 'center',
    fontSize: 32,
  },
  container: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    marginTop: 125,
    margin: '20%',
    justifyContent: 'space-between',
  },
});
