import { Box } from '@chakra-ui/react'
import React, { Component } from 'react'
import { ProductGrid } from '../../components/product'
import { Link } from 'react-router-dom'
export default class Home extends Component {
  render() {
    return (
      <Box display="flex" flexDirection={{ base: 'column', md: 'row' }}>
        <Box pb={{ base: 4, md: 0 }} flex={{ base: 'none', md: 3,lg:2 }}>
          <Link to="/test">Test left bar</Link>
        </Box>
        <Box mt={{ base: 4, md: 0 }} ml={{ base: 0, md: 4 }} flex={{ base: 'none', md: 9,lg:10 }}>
          <ProductGrid />
        </Box>
      </Box>
    )
  }
}
