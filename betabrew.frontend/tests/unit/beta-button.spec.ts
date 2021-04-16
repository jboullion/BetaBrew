import { mount } from '@vue/test-utils'
import BetaButton from '@/components/BetaButton.vue'

describe('BetaButton.vue', () => {
	it('renders correct number of button links', () => {
		const testText = 'click here!';

		const wrapper = mount(BetaButton, {
			propsData: { 
				link: false
			},
			slots: {
				default: testText
			}
		})

		expect(wrapper.find('button').text()).toBe(testText)
	})

	it('it has disabled button when disabled passed as prop', () => {
		const testText = 'disabled!';

		const wrapper = mount(BetaButton, {
			propsData: { 
				disabled: true
			},
			slots: {
				default: testText
			}
		})

		expect(wrapper.find('button:disabled'));
	})
})
