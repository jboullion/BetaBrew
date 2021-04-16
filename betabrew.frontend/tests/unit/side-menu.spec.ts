import { mount } from '@vue/test-utils'
import SideMenu from '@/components/Sidemenu.vue'

describe('SideMenu.vue', () => {
	it('renders correct number of button links', () => {
		const wrapper = mount(SideMenu, {
			propsData: {  },
			stubs: ['router-link'],
			slots: {}
		})

		expect(wrapper.findAll('button').length).toBe(4)
	})
})
