from mlagents_envs.environment import UnityEnvironment
from gym_unity.envs import UnityToGymWrapper

from stable_baselines3 import DQN
from wandb.integration.sb3 import WandbCallback
import wandb

config = {
    'policy_type': 'MlpPolicy',
    'total_timesteps': 25000
}
run = wandb.init(entity='raffael', project='unity_test', config=config, sync_tensorboard=True, monitor_gym=True, save_code=True)

env_name = None # None if in editor, else path to .exe
unity_env = UnityEnvironment(
    file_name=env_name, seed=1, side_channels=[])
env = UnityToGymWrapper(unity_env, allow_multiple_obs=False)

agent = DQN('MlpPolicy', env, verbose=2)

agent.learn(10000, callback=WandbCallback())

run.finish()

agent.save('./agents/dqn_unity')

input('Training finished')

# Test loop
episodes = 10
for ep in range(episodes):
    done = False
    obs = env.reset()
    while not done:
        action, states = agent.predict(obs, deterministic=True)
        obs, reward, done, info = env.step(action)
        